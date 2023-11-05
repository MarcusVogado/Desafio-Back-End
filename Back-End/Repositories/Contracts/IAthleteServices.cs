using Back_End.Models.Model;
using Microsoft.AspNetCore.Identity;

namespace Back_End.Repositories.Contracts
{
    public interface IAthleteServices
    {
        Task<Athlete?> CreateAthleteUser(Athlete athlete, string password);
        Task<Athlete> UpdateAthleteUser(Athlete athlete);
        Task<Athlete?> DeleteAthele(string athleteId);
        Athlete GetAthleteUser(string email, string password);
        Athlete GetAthleteByID(string id);
        List<Athlete> GetAllAthlete();

    }
}
