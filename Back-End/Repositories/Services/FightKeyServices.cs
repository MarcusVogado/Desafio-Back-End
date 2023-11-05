using AutoMapper;
using Back_End.DataContext;
using Back_End.Models.DTOsModels;
using Back_End.Models.Enums;
using Back_End.Models.Model;
using Back_End.Repositories.Contracts;

namespace Back_End.Repositories.Services
{
    public class FightKeyServices : IFightKeyServices
    {
        #region Dependencies Injections
        private readonly DbContextDataBase _dbContextFightKey;
        private readonly IAthleteServices _athleteServices;
        private readonly IMapper _mapper;
        public FightKeyServices(DbContextDataBase dbContextFightKey
            , IAthleteServices athleteServices
            , IMapper mapper
            )
        {
            _dbContextFightKey = dbContextFightKey;
            _athleteServices = athleteServices;
            _mapper = mapper;

        }
        #endregion

        #region CreatedFightKey
        public List<FightKey> CreatedFightKey(Championship championship)
        {
            var championshipResult = _dbContextFightKey.Championships.Find(championship.Id);
            //Categorias Masculinas
            FightKey fightKeyMasculinaMarromLeve = new FightKey
            {
                AthleteBurden = AthleteBurden.Leve,
                AthleteRange = AthleteRange.Marrom,
                AthleteSex = AthleteSex.Masculino,
                Status = FigthKeyStatus.Open,
                IDChampionship = championshipResult.Id

            };
            _dbContextFightKey.FightKeys.Add(fightKeyMasculinaMarromLeve);
            _dbContextFightKey.SaveChanges();

            FightKey fightKeyMasculinaMarromPesado = new FightKey
            {
                AthleteBurden = AthleteBurden.Pesado,
                AthleteRange = AthleteRange.Marrom,
                AthleteSex = AthleteSex.Masculino,
                Status = FigthKeyStatus.Open,
                IDChampionship = championshipResult.Id

            };
            _dbContextFightKey.FightKeys.Add(fightKeyMasculinaMarromPesado);
            _dbContextFightKey.SaveChanges();

            FightKey fightKeyMasculinaPretaLeve = new FightKey
            {
                AthleteBurden = AthleteBurden.Leve,
                AthleteRange = AthleteRange.Preta,
                AthleteSex = AthleteSex.Masculino,
                Status = FigthKeyStatus.Open,
                IDChampionship = championshipResult.Id

            };
            _dbContextFightKey.FightKeys.Add(fightKeyMasculinaPretaLeve);
            _dbContextFightKey.SaveChanges();

            FightKey fightKeyMasculinaPretaPesado = new FightKey
            {
                AthleteBurden = AthleteBurden.Pesado,
                AthleteRange = AthleteRange.Preta,
                AthleteSex = AthleteSex.Masculino,
                Status = FigthKeyStatus.Open,
                IDChampionship = championshipResult.Id

            };
            _dbContextFightKey.FightKeys.Add(fightKeyMasculinaPretaPesado);
            _dbContextFightKey.SaveChanges();


            //Categorias Feminas
            FightKey fightKeyFemininaMarromLeve = new FightKey
            {
                AthleteBurden = AthleteBurden.Leve,
                AthleteRange = AthleteRange.Marrom,
                AthleteSex = AthleteSex.Feminino,
                Status = FigthKeyStatus.Open,
                IDChampionship = championshipResult.Id

            };
            _dbContextFightKey.FightKeys.Add(fightKeyFemininaMarromLeve);
            _dbContextFightKey.SaveChanges();

            FightKey fightKeyFemininaMarronPesado = new FightKey
            {
                AthleteBurden = AthleteBurden.Pesado,
                AthleteRange = AthleteRange.Marrom,
                AthleteSex = AthleteSex.Feminino,
                Status = FigthKeyStatus.Open,
                IDChampionship = championshipResult.Id

            };
            _dbContextFightKey.FightKeys.Add(fightKeyFemininaMarronPesado);
            _dbContextFightKey.SaveChanges();

            FightKey fightKeyFemininaPretaLeve = new FightKey
            {
                AthleteBurden = AthleteBurden.Leve,
                AthleteRange = AthleteRange.Preta,
                AthleteSex = AthleteSex.Feminino,
                Status = FigthKeyStatus.Open,
                IDChampionship = championshipResult.Id

            };
            _dbContextFightKey.FightKeys.Add(fightKeyFemininaPretaLeve);
            _dbContextFightKey.SaveChanges();

            FightKey fightKeyFemininaPretaPesado = new FightKey
            {
                AthleteBurden = AthleteBurden.Pesado,
                AthleteRange = AthleteRange.Preta,
                AthleteSex = AthleteSex.Feminino,
                Status = FigthKeyStatus.Open,
                IDChampionship = championshipResult.Id

            };
            _dbContextFightKey.FightKeys.Add(fightKeyFemininaPretaPesado);
            _dbContextFightKey.SaveChanges();

            return GetFightKeys(championship.Id.ToString());
        }
        #endregion        

        #region GetFightKeyAllAthletesRegistrations
        public List<DTOAthlete> GetFightKeyAllAthletesRegistrations(Guid keyFigthId)
        {
            var listAthleteInscrition = _dbContextFightKey.ChampionshipRegistrations.Where(f => f.IDFightKey == keyFigthId).ToList();

            var listAthleteReturn = new List<DTOAthlete>();
            foreach (var inscrition in listAthleteInscrition)
            {
                var athlheteDate = _athleteServices.GetAthleteByID(inscrition.IDAthlete.ToString());
                if (athlheteDate != null)
                {

                    var athleteSaveList = _mapper.Map<DTOAthlete>(athlheteDate);
                    listAthleteReturn.Add(athleteSaveList);
                }
            }
            return listAthleteReturn;
        }
        #endregion

        #region FightPrizeDraw
        public List<Fight> FightPrizeDraw(Guid keyFightId)
        {
            var random = new Random();
            var keyFigthResult = GetFightKeyId(keyFightId);
            if (keyFigthResult == null) { return null; }
            var listAthleteInscrition = GetFightKeyAllAthletesRegistrations(keyFigthResult.Id);
            while (listAthleteInscrition.Count >= 2)
            {
                int indexAthleteOne = random.Next(listAthleteInscrition.Count);
                var athleteOne = listAthleteInscrition[indexAthleteOne];
                listAthleteInscrition.RemoveAt(indexAthleteOne);

                int indexAthleteTwo = random.Next(listAthleteInscrition.Count);
                var athleteTwo = listAthleteInscrition[indexAthleteTwo];
                listAthleteInscrition.RemoveAt(indexAthleteTwo);

                var newFight = new Fight
                {
                    IDFightKey = keyFigthResult.Id,
                    IDAthleteOne = athleteOne.Id,
                    IDAthleteTwo = athleteTwo.Id,
                };
                _dbContextFightKey.Fights.Add(newFight);
                _dbContextFightKey.SaveChanges();
            }
            return _dbContextFightKey.Fights.Where(f => f.IDFightKey == keyFightId).ToList();
        }
        #endregion        

        #region DeclareTheWinnerOfFight

        public Fight? DeclareTheWinnerOfFight(Guid fightId, Guid athleteId)
        {
            var fightResult = GetFigthId(fightId);
            if (fightResult == null) { return null; }
            if (fightResult.AthleteOne.Id == athleteId || fightResult.AthleteTwo.Id == athleteId)
            {
                fightResult.IDAthleteWinnerFight = athleteId;
                _dbContextFightKey.Fights.Update(fightResult);
                _dbContextFightKey.SaveChanges();
                return fightResult;
            }
            return null;
        }
        #endregion

        #region FightKeyResultFights
        public List<Fight> FightKeyResultFights(Guid fightId)
        {
            var listFightResults = _dbContextFightKey.Fights.Where(f => f.Id == fightId && f.AthleteWinner != null).ToList();
            if(listFightResults!=null)
            {
                return listFightResults;
            }
            return null;
        }
        #endregion

        #region FinalResultFightKey
        public List<Fight> FinalResultFightKey(Guid fightId)
        {
            var listFightResult = FightKeyResultFights(fightId).ToList();
            var resulFinalFightKey = listFightResult.Where(r=>
            !listFightResult.Any(r1 =>
             r1.IDAthleteOne == r.IDAthleteWinnerFight 
             ||r1.IDAthleteTwo ==r.IDAthleteWinnerFight )).ToList();

            return resulFinalFightKey;
        }
        #endregion

        #region GetFigthId
        public Fight? GetFigthId(Guid figthId)
        {
            return _dbContextFightKey.Fights.Find(figthId);
        }
        #endregion

        #region GetFightKeys
        public List<FightKey> GetFightKeys(string championshipId)
        {
            return _dbContextFightKey.FightKeys.Where(f => f.IDChampionship.ToString() == championshipId).ToList();
        }
        #endregion

        #region GetFightKeyId
        public FightKey? GetFightKeyId(Guid fightKeyId)
        {
            return _dbContextFightKey.FightKeys.FirstOrDefault(f => f.Id == fightKeyId);
        }
        #endregion
    }
}
