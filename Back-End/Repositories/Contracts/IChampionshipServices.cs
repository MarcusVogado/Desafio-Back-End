using Back_End.Models.Model;
using Microsoft.AspNetCore.Identity;

namespace Back_End.Repositories.Contracts
{
    public interface IChampionshipServices
    {
        Championship CreateChampionship(Championship championship);
        Championship? UpdateChampionship(Championship championship);        
        Championship GetChampionshipByID(string id);
        List<Championship> GetAllChampionShip(int skip, int take);
        Championship DeleteChampionship(string id);
        Championship? GetChampionshipExists(Championship championship);
    }
}
