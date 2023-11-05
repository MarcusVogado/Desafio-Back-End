using Back_End.DataContext;
using Back_End.Models.DTOsModels;
using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Back_End.Repositories.Services
{
    public class WebTokenServices : IWebTokenServices
    {
        private readonly DbContextDataBase _dataBaseContext;
        private readonly IConfiguration _configuration;
        public WebTokenServices(DbContextDataBase dataBaseContext, IConfiguration configuration)
        {
            _dataBaseContext = dataBaseContext;
            _configuration = configuration;
        }
        public WebToken CreateWebToken(WebToken webToken)
        {
            _dataBaseContext.WebTokens.Add(webToken);
            _dataBaseContext.SaveChanges();
            return webToken;
        }
        public WebToken? GetWebToken(string refreshToken)
        {
            return _dataBaseContext.WebTokens.FirstOrDefault(a => a.RefreshToken == refreshToken);
        }
        public WebToken? RefreshToken(string refreshToken)
        {
            var tokenResult = GetWebToken(refreshToken);
            if (tokenResult != null)
            {
                tokenResult.Atualizado = DateTime.Now;
                tokenResult.ExpirationRefreshToken = DateTime.UtcNow.AddHours(2);
                tokenResult.ExpirationToken = DateTime.UtcNow.AddHours(2);
                _dataBaseContext.Update(tokenResult);
                _dataBaseContext.SaveChanges();
                return tokenResult;
            }
            return null;
        }

        public WebToken? GetUserWebToken(string idUser)
        {
            if (string.IsNullOrEmpty(idUser))
            {
                return _dataBaseContext.WebTokens.FirstOrDefault(tk => tk.IDUser == idUser);
            }
            return null;

        }

        public bool DeleteWebToken(string webToken)
        {
            var tokenResult = GetWebToken(webToken);
            if (tokenResult != null)
            {
                _dataBaseContext.WebTokens.Remove(tokenResult);
                var resulDeleteToken = _dataBaseContext.SaveChanges();
                return (resulDeleteToken > 0) ? true : false;
            }
            return false;
        }

        public DTOWebToken BuildTokenAdm(IdentityUser user)
        {
            var claims = new[]
            {
                new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email,user.Email),
               new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,user.Id)
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Tokenkey:Token").Value));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddHours(2);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: exp,
                signingCredentials: sign
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid().ToString();
            var expRefreshToken = DateTime.UtcNow.AddHours(2);
            var tokenDTO = new DTOWebToken { Token = tokenString, ExpirationToken = exp, ExpirationRefreshToken = expRefreshToken, RefreshToken = refreshToken };
            return tokenDTO;
        }

        public DTOWebToken BuildTokenAthlete(Athlete user)
        {
            var claims = new[]
            {
               new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Email,user.Email),
               new Claim(System.IdentityModel.Tokens.Jwt.JwtRegisteredClaimNames.Sub,user.Id.ToString())
            };
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration.GetSection("Tokenkey:Token").Value));
            var sign = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var exp = DateTime.UtcNow.AddHours(2);

            JwtSecurityToken token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: claims,
                expires: exp,
                signingCredentials: sign
                );

            var tokenString = new JwtSecurityTokenHandler().WriteToken(token);
            var refreshToken = Guid.NewGuid().ToString();
            var expRefreshToken = DateTime.UtcNow.AddHours(2);
            var tokenDTO = new DTOWebToken { Token = tokenString, ExpirationToken = exp, ExpirationRefreshToken = expRefreshToken, RefreshToken = refreshToken };
            return tokenDTO;
        }

    }
}

