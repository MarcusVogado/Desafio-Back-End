using AutoMapper;
using Back_End.Models.DTOsModels;
using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserAuthenticationController : ControllerBase
    {
        #region Dependencies Injection
        private readonly IWebTokenServices _webTokenServices;
        private readonly IAthleteServices _userAthleteServices;
        private readonly IUserAdmServices _userAdmServices;
        private readonly IMapper _mapper;
        public UserAuthenticationController(IWebTokenServices webTokenServices, IUserAdmServices userAdmServices, IMapper mapper,
          IAthleteServices thleteServices, IConfiguration configuration)
        {            
            _webTokenServices = webTokenServices;
            _userAthleteServices = thleteServices;
            _userAdmServices = userAdmServices;
            _mapper = mapper;
        }
        #endregion

        #region AthleteAuthenticate
        [HttpPost("athlete/authentication")]
        public IActionResult AthleteAuthenticate([FromBody] DTOUserLogin userAthlete)
        {
            if (ModelState.IsValid)
            {
                var userResult = _userAthleteServices.GetAthleteUser(userAthlete.Email, userAthlete.Password);
                if (userResult is not null)
                {

                    var tokenGenerated = _webTokenServices.BuildTokenAthlete(userResult);
                    var token = new WebToken()
                    {
                        Token = tokenGenerated.Token,
                        ExpirationToken = tokenGenerated.ExpirationToken,
                        RefreshToken = tokenGenerated.RefreshToken,
                        ExpirationRefreshToken = tokenGenerated.ExpirationRefreshToken,
                        IDUser = userResult.Id.ToString(),
                        Criado = DateTime.Now,

                    };
                    _webTokenServices.CreateWebToken(token);
                    var webTokenDTO = _mapper.Map<DTOWebToken>(token);
                    var userAthleteDTO = _mapper.Map<DTOAthlete>(userResult);
                    return Ok(new JsonResult(webTokenDTO,userAthleteDTO));

                }
                else
                {
                    return BadRequest("Usuário não localizado");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }

        }
        #endregion

        #region AdmUserAuthenticate
        [HttpPost("admuser/authentication")]
        public IActionResult AdmUserAuthenticate([FromBody] DTOUserLogin userAdm)
        {
            if (ModelState.IsValid)
            {
                var userResult = _userAdmServices.GetAdmUser(userAdm.Email, userAdm.Password);
                if (userResult is not null)
                {
                    var tokenGenerated = _webTokenServices.BuildTokenAdm(userResult);
                    var token = new WebToken()
                    {
                        Token = tokenGenerated.Token,
                        ExpirationToken = tokenGenerated.ExpirationToken,
                        RefreshToken = tokenGenerated.RefreshToken,
                        ExpirationRefreshToken = tokenGenerated.ExpirationRefreshToken,
                        IDUser = userResult.Id,
                        Criado = DateTime.Now,

                    };
                    _webTokenServices.CreateWebToken(token);
                    var webTokenDTO = _mapper.Map<DTOWebToken>(token);
                    var userAdmDTO = _mapper.Map<DTOUserAdmReturn>(userResult);
                    return Ok(new JsonResult(webTokenDTO,userAdmDTO));

                }
                else
                {
                    return BadRequest("Usuário não localizado");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region RefreshToken
        [HttpPost("tokenRefresh")]
        public IActionResult RefreshToken([FromBody] string refreshToken)
        {
            var tokenResult = _webTokenServices.RefreshToken(refreshToken);
            if (tokenResult == null)
            {
                return NotFound("Token Não Existe Faça Login Novamente");
            }
            var webToken = _mapper.Map<DTOWebToken>(tokenResult);
            return Ok(new JsonResult(webToken));
        }
        #endregion
    }
}
