using Back_End.DataContext;
using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

public class AthleteServices : IAthleteServices
{
    private readonly DbContextDataBase _dbContextDataBase;
    private readonly PasswordHasher<Athlete> _passwordHasher;

    public AthleteServices(DbContextDataBase dbContextDataBase)
    {
        _dbContextDataBase = dbContextDataBase;
        _passwordHasher = new PasswordHasher<Athlete>();
    }

    public async Task<Athlete?> CreateAthleteUser(Athlete athlete, string password)
    {
        var existingAthlete = await _dbContextDataBase.Athletes.FirstOrDefaultAsync(a => a.Email == athlete.Email);
        if (existingAthlete != null)
        {
            return null;
        }

        var hashedPassword = _passwordHasher.HashPassword(athlete, password);
        athlete.Password = hashedPassword;

        _dbContextDataBase.Athletes.Add(athlete);
        await _dbContextDataBase.SaveChangesAsync();

        return athlete;
    }

    public async Task<Athlete> UpdateAthleteUser(Athlete athlete)
    {
        _dbContextDataBase.Athletes.Update(athlete);
        await _dbContextDataBase.SaveChangesAsync();
        return athlete;
    }  

    public async Task<Athlete?> DeleteAthele(string id)
    {
        var athlete = await _dbContextDataBase.Athletes.FirstOrDefaultAsync(a => a.Id.ToString() == id);
        if (athlete == null)
        {
            return null;
        }

        _dbContextDataBase.Athletes.Remove(athlete);
        await _dbContextDataBase.SaveChangesAsync();

        return athlete;
    }

    public Athlete GetAthleteUser(string email, string password)
    {
        var athlete = _dbContextDataBase.Athletes.FirstOrDefault(a => a.Email == email);
        if (athlete == null)
        {
            return null;
        }

        var result = _passwordHasher.VerifyHashedPassword(athlete, athlete.Password, password);
        if (result != PasswordVerificationResult.Success)
        {
            return null;
        }

        return athlete;
    }

    public List<Athlete> GetAllAthlete()
    {
        return  _dbContextDataBase.Athletes.ToList();
    }

    public  Athlete? GetAthleteByID(string id)
    {
        return _dbContextDataBase.Athletes.FirstOrDefault(a => a.Id.ToString() == id);
    }   

}

