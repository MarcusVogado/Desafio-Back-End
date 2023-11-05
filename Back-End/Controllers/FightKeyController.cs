using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using Back_End.Repositories.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FightKeyController : ControllerBase
    {
        #region Dependencies Injection
        private readonly IFightKeyServices _fightKeyServices;
        private readonly IChampionshipServices _chapionshipServices;

        public FightKeyController(IFightKeyServices fightKeyServices, IChampionshipServices championshipServices)
        {
            _fightKeyServices = fightKeyServices;
            _chapionshipServices = championshipServices;
        }
        #endregion

        #region GetForKeysChampionship        
        [HttpGet]
        [Route("getFightKeys")]
        public IActionResult GetForKeysChampionship(string championshipId)
        {
            var listFightKeys = _fightKeyServices.GetFightKeys(championshipId).ToList();
            if (listFightKeys == null)
            {
                return BadRequest("Campeonato não Encontrado");
            }
            return Ok(new JsonResult(listFightKeys));
        }
        #endregion

        #region GetFightKeyAthletesRegistrations
        [Authorize(Policy = "Bearer")]
        [HttpGet]
        [Route("getfightkeyathletesRegistration")]
        public IActionResult GetFightKeyAthletesRegistrations(Guid keyFightId)
        {
            try
            {
                var listAthleteInscrition = _fightKeyServices.GetFightKeyAllAthletesRegistrations(keyFightId);
                return Ok(new JsonResult(listAthleteInscrition));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        #endregion

        #region PrizeDrawFight
        [Authorize(Policy = "Bearer")]
        [HttpPost]
        [Route("prizeDrawFight")]
        public IActionResult PrizeDrawFight(Guid fightKeyId)
        {
            try
            {
                var resulPrizeDraw = _fightKeyServices.FightPrizeDraw(fightKeyId);
                return Ok(new JsonResult(resulPrizeDraw));
            }
            catch (Exception ex)
            {
                return BadRequest($"Não foi possível realizar o sorteio da chave: ${ex.Message}");
            }
        }
        #endregion

        #region DeclareTheWinnerOfFight
        [Authorize(Policy = "Bearer")]
        [HttpPut]
        [Route("declareTheWinnerOfFight")]
        public IActionResult DeclareTheWinnerOfFight(Guid fightKeyId, Guid athleteWinnerFight)
        {
            try
            {
                var declareWinnerResult = _fightKeyServices.DeclareTheWinnerOfFight(fightKeyId, athleteWinnerFight);
                return Ok(new JsonResult(declareWinnerResult));

            }catch(Exception ex)
            {
                return BadRequest($"Erro ao declarar Vencedor ${ex.Message}");
            }
        }
        #endregion
    }
}
