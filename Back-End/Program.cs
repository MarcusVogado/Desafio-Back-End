using Back_End.DataContext;
using Back_End.Models.Model;
using Back_End.Profiles;
using Back_End.Repositories.Contracts;
using Back_End.Repositories.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Por favor informe um token válido",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

//DbContext

builder.Services.AddDbContext<DbContextDataBase>(op =>
{
    op.UseSqlServer(builder.Configuration.GetConnectionString("DbConectionDefault"));
});
//Email Config
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("SmptConfig"));

//Identity User Configuração

builder.Services.AddIdentity<UserAdm, IdentityRole>(options =>
{
    options.Password.RequireUppercase = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireDigit = false;
})
.AddEntityFrameworkStores<DbContextDataBase>()
.AddDefaultTokenProviders();


//Autenticação com JWT

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateAudience = false,
        ValidateIssuer = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["TokenKey:Token"])),
    };
});

builder.Services.AddAuthorization(auth =>
{
    auth.AddPolicy("Bearer", new AuthorizationPolicyBuilder()
        .AddAuthenticationSchemes(JwtBearerDefaults.AuthenticationScheme)
        .RequireAuthenticatedUser()
        .Build());
});

builder.Services.ConfigureApplicationCookie(options =>
{
    options.Events.OnRedirectToLogin = context =>
    {
        context.Response.StatusCode = 401;
        return Task.CompletedTask;
    };
});

//Auto Mapper

builder.Services.AddAutoMapper(option =>
{
    option.AddProfile<ProfileWebToken>();
    option.AddProfile<ProfileUserAdm>();
    option.AddProfile<ProfileAthlete>();
});

//Injeção de dependencias

builder.Services.AddScoped<IUserAdmServices, UserAdmServices>();
builder.Services.AddScoped<IAthleteServices, AthleteServices>();
builder.Services.AddScoped<IWebTokenServices, WebTokenServices>();
builder.Services.AddScoped<IChampionshipServices, ChampionshipServices>();
builder.Services.AddScoped<IFightKeyServices, FightKeyServices>();
builder.Services.AddScoped<IChampionshipRegistrationServices, ChampionshipRegistrationServices>();
builder.Services.AddTransient<IEmailServices, EmailServices>();

//CORS 

//builder.Services.AddCors(options =>
//{
//    options.AddDefaultPolicy(builder =>
//    {
//        builder.WithOrigins("http://127.0.0.1:5500")
//               .AllowAnyHeader()
//               .AllowAnyMethod();
//    });
//});

var app = builder.Build();

// Configure the HTTP request pipeline

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseAuthentication();

app.MapControllers();

app.UseCors();

app.Run();
