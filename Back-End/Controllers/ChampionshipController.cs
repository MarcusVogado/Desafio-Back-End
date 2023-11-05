using AutoMapper;
using Back_End.Models.DTOsModels;
using Back_End.Models.Enums;
using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Back_End.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ChampionshipController : ControllerBase
    {
        #region Dependecies Injection
        private readonly IChampionshipServices _championshipService;
        private readonly IUserAdmServices _userAdmService;
        private readonly IChampionshipRegistrationServices _championshipRegistrationService;
        private readonly IAthleteServices _thleteService;
        private readonly IMapper _mapper;

        public ChampionshipController(IChampionshipServices championshipService
            , IUserAdmServices userAdmService
            , IChampionshipRegistrationServices championshipRegistrationService
            , IAthleteServices athleteServices
            , IMapper mapper
            )
        {
            _championshipService = championshipService;
            _userAdmService = userAdmService;
            _championshipRegistrationService = championshipRegistrationService;
            _thleteService = athleteServices;
            _mapper = mapper;
        }
        #endregion

        #region CreateChampionship
        [Authorize]
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateChampionship([FromForm] Championship championship, [FromForm] string userAdmId)
        {
            var userAdmResult = _userAdmService.GetAdmByID(userAdmId);
            if (userAdmResult == null)
            {
                return BadRequest("Usuário não encontrado");
            }
            if (userAdmResult.UserAdmType == Models.Enums.UserAdmType.FullAdm)
            {
                var championshipExist = _championshipService.GetChampionshipExists(championship);
                if (championshipExist == null)
                {
                    if (championship.ImageFile != null && championship.ImageFile.Length != 0)
                    {
                        var fileName = Guid.NewGuid() + Path.GetExtension(championship.ImageFile.FileName);
                        var fullImagePath = Path.Combine(Environment.CurrentDirectory + "\\ChampionshipImagesCards\\", fileName);

                        using (var streamImage = new FileStream(fullImagePath, FileMode.Create))
                        {
                            await championship.ImageFile.CopyToAsync(streamImage);
                        }

                        if (ModelState.IsValid)
                        {
                            var championshipCreate = new Championship
                            {
                                TitleChampionship = championship.TitleChampionship,
                                ImagePath = fullImagePath,
                                City = championship.City,
                                State = championship.State,
                                DateOfRealization = championship.DateOfRealization,
                                Gymnasium = championship.Gymnasium,
                                GeneralInformation = championship.GeneralInformation,
                                PublicEntrance = championship.PublicEntrance,
                                TypeOfChampionship = championship.TypeOfChampionship,
                                StageOfChampionship = championship.StageOfChampionship,
                                Status = true
                            };

                            var championshipResult = _championshipService.CreateChampionship(championshipCreate);

                            return Ok(new JsonResult(championshipResult));
                        }
                        else
                        {
                            return BadRequest(ModelState);
                        }
                    }
                    else
                    {
                        return BadRequest("Imagem não enviada");
                    }
                }
                else
                {
                    return BadRequest("O Campeonato já foi cadastrado");
                }
            }
            else
            {
                return Unauthorized("Usuário não autorizado");
            }

        }
        #endregion

        #region UpdateChampionship
        [Authorize]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateChampionship([FromForm] Championship championship, [FromForm] string userAdmId)
        {
            try
            {
                var userAdmResult = _userAdmService.GetAdmByID(userAdmId);
                if (userAdmResult == null)
                {
                    return BadRequest("Usuário não encontrado");
                }
                if (userAdmResult.UserAdmType == Models.Enums.UserAdmType.FullAdm)
                {
                    var championshipExist = _championshipService.GetChampionshipExists(championship);
                    if (championshipExist == null)
                    {
                        if (championship.ImageFile != null && championship.ImageFile.Length != 0)
                        {
                            var fileName = Guid.NewGuid() + Path.GetExtension(championship.ImageFile.FileName);
                            var fullImagePath = Path.Combine(Environment.CurrentDirectory + "\\ChampionshipImagesCards\\", fileName);

                            using (var streamImage = new FileStream(fullImagePath, FileMode.Create))
                            {
                                await championship.ImageFile.CopyToAsync(streamImage);
                            }

                            if (ModelState.IsValid)
                            {
                                var championshipCreate = new Championship
                                {
                                    TitleChampionship = championship.TitleChampionship,
                                    ImagePath = fullImagePath,
                                    City = championship.City,
                                    State = championship.State,
                                    DateOfRealization = championship.DateOfRealization,
                                    Gymnasium = championship.Gymnasium,
                                    GeneralInformation = championship.GeneralInformation,
                                    PublicEntrance = championship.PublicEntrance,
                                    TypeOfChampionship = championship.TypeOfChampionship,
                                    StageOfChampionship = championship.StageOfChampionship,
                                    Status = true
                                };

                                var championshipResult = _championshipService.UpdateChampionship(championshipCreate);

                                return Ok(new JsonResult(championshipResult));
                            }
                            else
                            {
                                return BadRequest(ModelState);
                            }
                        }
                        else
                        {
                            return BadRequest("Imagem não enviada");
                        }
                    }
                    else
                    {
                        return BadRequest("O Campeonato já foi cadastrado");
                    }
                }
                else
                {
                    return Unauthorized("Usuário não autorizado");
                }

            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }


        }
        #endregion

        #region DeleteChampionsip
        [Authorize]
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteChampionsip([FromForm]string championshipId,[FromForm] string userAdmId)
        {
            try
            {
                var userResult = _userAdmService.GetAdmByID(userAdmId);
                if (userResult == null) { return BadRequest("Usuário não encontrado"); }
                if (userResult.UserAdmType == UserAdmType.FullAdm)
                {
                    var championshipResult = _championshipService.DeleteChampionship(championshipId);
                    if (championshipResult == null) { return BadRequest("Campeonato não existe"); }
                    return Ok("Campeonato excluido" + championshipResult);
                }
                else
                {
                    return BadRequest("Usuário não encontrado ou não autorizado");
                }
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }


        }
        #endregion

        #region GetAllChampionships        
        [HttpGet]
        [Route("getAllChampionship")]
        public IActionResult GetAllChampionships([FromQuery] int skip, [FromQuery] int take)
        {
            try
            {
                var listChampionship = _championshipService.GetAllChampionShip(skip, take);
                var listChapionshipOrdeByDate = listChampionship.OrderByDescending(c => c.DateOfRealization);

                return Ok(new JsonResult(listChapionshipOrdeByDate));
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }

        }
        #endregion

        #region RegistrationAthleteChampionship
        [Authorize]
        [HttpPost]
        [Route("registrationAthlete")]
        public IActionResult RegistrationAthleteChampionship([FromForm] Guid athleteId, [FromForm] Guid keyFightId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var athleteResult = _thleteService.GetAthleteByID(athleteId.ToString());
                    if (athleteResult == null) { return BadRequest("Usuário não encontrado"); }

                    athleteResult.Password = "";
                    var registrationFightResult = _championshipRegistrationService.CreateChampionshipRegistrationServices(athleteResult, keyFightId);
                    if (registrationFightResult != null)
                    {
                        return Ok(new JsonResult(registrationFightResult));
                    }
                    else
                    {
                        return BadRequest("O usuário não pode ser cadastrado nesta chave");
                    }
                }
                return BadRequest("Todos os dados precisam ser preenchidos");
            }
            catch (Exception erro)
            {
                return BadRequest(erro.Message);
            }
        }
        #endregion

    }
}
