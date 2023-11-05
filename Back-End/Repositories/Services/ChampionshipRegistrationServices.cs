using AutoMapper;
using Back_End.DataContext;
using Back_End.Models.DTOsModels;
using Back_End.Models.Enums;
using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using System.Drawing;

namespace Back_End.Repositories.Services
{
    public class ChampionshipRegistrationServices : IChampionshipRegistrationServices
    {
        #region Dependencies Injection
        private readonly DbContextDataBase _dbContextRegistration;
        private readonly IFightKeyServices _fightKeyServices;        
        
        public ChampionshipRegistrationServices(
            DbContextDataBase dbContextRegistration
            , IFightKeyServices fightKeyServices            
            , IMapper mapper)
        {
            _dbContextRegistration = dbContextRegistration;
            _fightKeyServices = fightKeyServices; 
        }
        #endregion

        #region CreateChampionshipRegistrationServices
        public ChampionshipRegistration? CreateChampionshipRegistrationServices(Athlete athlete, Guid keyFightId)
        {
            var keyFigthResult = _fightKeyServices.GetFightKeyId(keyFightId);
            
            

            if (keyFigthResult == null || keyFigthResult.Status== FigthKeyStatus.Close) { return null; }

            if (keyFigthResult.AthleteRange == athlete.AthleteRange
                && keyFigthResult.AthleteBurden == athlete.AthleteBurden
                && keyFigthResult.AthleteSex == athlete.AthleteSex)
            {
                var athleteRegistration = new ChampionshipRegistration
                {
                    ChampionshipRegistrationStatus = ChampionshipRegistrationStatus.EmAnalise,
                    DateOfRegistration = DateTime.UtcNow,
                    IDAthlete = athlete.Id,
                    IDFightKey = keyFigthResult.Id
                };
                _dbContextRegistration.ChampionshipRegistrations.Add(athleteRegistration);
                _dbContextRegistration.SaveChanges();
                return athleteRegistration;
            }
            else
            {
                return null;
            }
        }
        #endregion       
    }
}
