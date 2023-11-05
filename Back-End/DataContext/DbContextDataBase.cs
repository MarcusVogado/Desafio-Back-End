using Back_End.Models.Enums;
using Back_End.Models.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Back_End.DataContext
{
    public class DbContextDataBase : IdentityDbContext<IdentityUser>
    {
        public DbContextDataBase(DbContextOptions<DbContextDataBase> options) : base(options)
        {

        }
        public DbSet<UserAdm> UserAdms { get; set; }
        public DbSet<Athlete> Athletes { get; set; }
        public DbSet<Championship> Championships { get; set; }
        public DbSet<ResultChampionship> ResultChampionships { get; set; }
        public DbSet<FightKey> FightKeys { get; set; }
        public DbSet<Fight> Fights { get; set; }
        public DbSet<WebToken> WebTokens { get; set; }
        public DbSet<ChampionshipRegistration> ChampionshipRegistrations { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Athlete>().HasKey(e => e.Id);
            modelBuilder.Entity<Athlete>().Property(e => e.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Championship>().HasKey(e => e.Id);
            modelBuilder.Entity<Championship>().Property(e => e.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<FightKey>().HasKey(e => e.Id);
            modelBuilder.Entity<FightKey>().Property(e => e.Id).ValueGeneratedOnAdd();

            modelBuilder.Entity<Fight>().HasKey(e => e.Id);
            modelBuilder.Entity<Fight>().Property(e => e.Id).ValueGeneratedOnAdd();
           
            modelBuilder.Entity<Fight>()
            .HasOne(fight => fight.AthleteOne)
            .WithMany()
            .HasForeignKey(fight => fight.IDAthleteOne)
            .OnDelete(DeleteBehavior.NoAction);
            
            modelBuilder.Entity<Fight>()
           .HasOne(fight => fight.AthleteTwo)
           .WithMany()
           .HasForeignKey(fight => fight.IDAthleteTwo)
           .OnDelete(DeleteBehavior.NoAction);

            modelBuilder.Entity<Fight>()
           .HasOne(fight => fight.AthleteWinner)
           .WithMany()
           .HasForeignKey(fight => fight.IDAthleteWinnerFight)
           .OnDelete(DeleteBehavior.NoAction);


            modelBuilder.Entity<ChampionshipRegistration>().HasKey(e => e.Id);
            modelBuilder.Entity<ChampionshipRegistration>().Property(e => e.Id).ValueGeneratedOnAdd();



            base.OnModelCreating(modelBuilder);
        }

    }
}
