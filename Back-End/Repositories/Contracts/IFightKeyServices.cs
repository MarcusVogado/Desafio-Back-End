using Back_End.Models.DTOsModels;
using Back_End.Models.Model;

namespace Back_End.Repositories.Contracts
{
    public interface IFightKeyServices
    {
        List<FightKey> CreatedFightKey(Championship championship);        
        List<Fight> FightPrizeDraw(Guid keyFightId);
        Fight? DeclareTheWinnerOfFight(Guid FightId, Guid athleteId);
        List<Fight> FightKeyResultFights(Guid fightId);
        FightKey? GetFightKeyId(Guid fightKeyId);
        List<DTOAthlete> GetFightKeyAllAthletesRegistrations(Guid keyFigthId);
        List<FightKey> GetFightKeys(string championshipId);
        Fight? GetFigthId(Guid fightId);
    }
}
