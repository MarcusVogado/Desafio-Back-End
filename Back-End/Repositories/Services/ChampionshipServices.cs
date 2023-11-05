using Back_End.DataContext;
using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using System.Data.Entity;

namespace Back_End.Repositories.Services
{
    public class ChampionshipServices : IChampionshipServices
    {
        private readonly DbContextDataBase _dbContextChampionship;
        private readonly IFightKeyServices _fightKeyServices;
        public ChampionshipServices(DbContextDataBase dbContextChampionship, IFightKeyServices fightKeyServices)
        {
            _dbContextChampionship = dbContextChampionship;
            _fightKeyServices = fightKeyServices;
        }

        public Championship CreateChampionship(Championship championship)
        {
            _dbContextChampionship.AddAsync(championship);
            _dbContextChampionship.SaveChanges();
            _fightKeyServices.CreatedFightKey(championship);
            return championship;
        }
        public Championship? UpdateChampionship(Championship championship)
        {           
            _dbContextChampionship.Championships.Update(championship);
            _dbContextChampionship.SaveChanges();
            return championship;
        }

        public Championship DeleteChampionship(string id)
        {
            var championshipResult = GetChampionshipByID(id);
            if (championshipResult == null)
            {
                return null;
            }
            _dbContextChampionship.Championships.Remove(championshipResult);
            _dbContextChampionship.SaveChanges();
            return championshipResult;
        }

        public List<Championship> GetAllChampionShip( int skyp, int take)
        {
            return _dbContextChampionship.Championships.Skip(skyp).Take(take).ToList();
        }

        public Championship? GetChampionshipByID(string id)
        {
            if (String.IsNullOrEmpty(id)) return null;
            return _dbContextChampionship.Championships.FirstOrDefault(c => c.Id.ToString() == id);
        }



        public Championship? GetChampionshipExists(Championship championship)
        {
            return _dbContextChampionship.Championships.FirstOrDefault(c =>
            c.TitleChampionship == championship.TitleChampionship
            && c.DateOfRealization == championship.DateOfRealization
            && c.City == championship.City
            && c.State == championship.State
            );
        }
    }
}
