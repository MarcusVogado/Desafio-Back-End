using AutoMapper;
using Back_End.Models.DTOsModels;
using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class AthleteController : ControllerBase
    {
        

        #region Dependencies Injection
        private readonly IAthleteServices _athleteServices;
        private readonly IMapper _mapper;
        private readonly IEmailServices _emailServices;
        
        public AthleteController(IAthleteServices athleteServices
            , IMapper mapper
            , IEmailServices emailServices
            )
        {
            _athleteServices = athleteServices;
            _mapper = mapper;
            _emailServices = emailServices;
                   
        }
        #endregion

        #region CreateAthlete       
        [HttpPost]
        [Route("create")]
        public async Task<IActionResult> CreateAthlete([FromBody] Athlete athleteUser)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var athlete = new Athlete
                    {
                        Email = athleteUser.Email,
                        FullName = athleteUser.FullName,
                        DateOfBirth = athleteUser.DateOfBirth,
                        CPF = athleteUser.CPF,
                        AthleteSex = athleteUser.AthleteSex,
                        Team = athleteUser.Team,
                        AthleteRange = athleteUser.AthleteRange,
                        AthleteBurden = athleteUser.AthleteBurden,
                        RegistrationDate = DateTime.UtcNow
                    };
                    
                    var athleteCreate = await _athleteServices.CreateAthleteUser(athlete, athleteUser.Password);
                    if(athleteCreate==null)
                    {
                        return BadRequest("Usuário já Cadastrado");
                    }
                    var athleteDTO = _mapper.Map<DTOAthlete>(athleteCreate);
                    //Envio de Email, Aviso de cadastro de novo athlete
                    //await _emailServices.SendToEmail("Cadastro de Atleta confirmado", $"Dados do Atleta ${JsonSerializer.Serialize(athleteDTO)}");
                                                            
                    return Ok(new JsonResult(athleteDTO));
                }
                else
                {
                    return BadRequest(ModelState);
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao criar o atleta: {ex.Message}");
            }
        }
        #endregion

        #region UpdateAthlete
        [Authorize(Policy = "Bearer")]
        [HttpPut]
        [Route("update")]
        public async Task<IActionResult> UpdateAthlete([FromBody] Athlete athleteUser)
        {
            try
            {
                var athlete = _athleteServices.GetAthleteByID(athleteUser.Id.ToString());
                if (athlete == null)
                {
                    return NotFound("Atleta não encontrado.");
                }
                var athleteUpdate = new Athlete
                {
                    Email = athleteUser.Email,
                    FullName = athleteUser.FullName,
                    DateOfBirth = athleteUser.DateOfBirth,
                    CPF = athleteUser.CPF,
                    AthleteSex = athleteUser.AthleteSex,
                    Team = athleteUser.Team,
                    AthleteRange = athleteUser.AthleteRange,
                    AthleteBurden = athleteUser.AthleteBurden,
                    RegistrationDate = athleteUser.RegistrationDate
                };

                await _athleteServices.UpdateAthleteUser(athleteUpdate);
                var athleteDTO = _mapper.Map<DTOAthlete>(athleteUpdate);
                return Ok(new JsonResult(athleteDTO));
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao atualizar o atleta: {ex.Message}");
            }
        }
        #endregion

        #region GetAthleteID
        [Authorize(Policy = "Bearer")]
        [HttpGet]
        [Route("athleteget/{id}")]
        public async Task<IActionResult> GetAthleteId(string id)
        {
            try
            {
                var athlete = _athleteServices.GetAthleteByID(id);
                if (athlete == null)
                {
                    return NotFound("Atleta não encontrado.");
                }
                var athleteDTO = _mapper.Map<DTOAthlete>(athlete);
                return Ok(new JsonResult(athleteDTO));
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao buscar o atleta: {ex.Message}");
            }
        }

        #endregion

        #region GetAllAthletes
        [Authorize(Policy = "Bearer")]
        [HttpGet]
        [Route("getAllAthletes")]
        public IActionResult GetAllAthletes()
        {
            try
            {
                var listAthletes = _athleteServices.GetAllAthlete();
                var listAthletesDTOs = new List<DTOAthlete>();
                foreach (var athlete in listAthletes)
                {
                    listAthletesDTOs.Add(_mapper.Map<DTOAthlete>(athlete));
                }
                return Ok(new JsonResult(listAthletesDTOs));

            }
            catch(Exception ex)
            {
                return BadRequest($"Erro ao listar atletas ${ex.Message}");
            }           
        }


        #endregion

        #region DeleteAthlete
        [Authorize(Policy = "Bearer")]
        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteAthlete(string id)
        {
            try
            {
                var athlete = _athleteServices.GetAthleteByID(id);
                if (athlete == null)
                {
                    return NotFound("Atleta não encontrado.");
                }
                await _athleteServices.DeleteAthele(id);
                return Ok("Atleta excluído com sucesso.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Erro ao excluir o atleta: {ex.Message}");
            }
        }
        #endregion

    }
}

