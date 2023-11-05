using Back_End.Models.DTOsModels;
using Back_End.Models.Enums;
using Back_End.Models.Model;
using Back_End.Repositories.Services;

namespace Back_End.Repositories.Contracts
{
    public interface IChampionshipRegistrationServices
    {
        
        ChampionshipRegistration? CreateChampionshipRegistrationServices(Athlete athlete, Guid keyFightId);       

    }
}
