using AutoMapper;
using Back_End.Models.DTOsModels;
using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Back_End.Controllers
{
    [ApiController]
    [Route("/[controller]")]
    public class UserAdmController : ControllerBase
    {
        #region Dependencies Injection
        private readonly IUserAdmServices _userAdmservices;
        private readonly IMapper _mapper;
        public UserAdmController(IUserAdmServices userAdmServices, IMapper mapper )
        {
            _userAdmservices = userAdmServices;
            _mapper = mapper;
        }
        #endregion

        #region CreateUserAdm
        [HttpPost]
        [Route("createUser")]
        public IActionResult CreateUserAdm([FromBody] DTOUserAdmCreate userAdmDTO)
        {

            if (ModelState.IsValid)
            {
                var userAdmCreate = new UserAdm();
                userAdmCreate.FullName = userAdmDTO.FullName;
                userAdmCreate.Email = userAdmDTO.Email;
                userAdmCreate.UserName = userAdmDTO.Email;

                var userResult = _userAdmservices.CreateAdmUser(userAdmCreate, userAdmDTO.Password);
                if (!userResult.Succeeded)
                {
                    List<string> erros = new List<string>();
                    foreach (var erro in userResult.Errors)
                    {
                        erros.Add(erro.Description);
                    }
                    return UnprocessableEntity(erros);
                }
                return Ok("Usuário cadastrado com sucesso");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region UpdateUserAdm
        [Authorize(Policy = "Bearer")]
        [HttpPut]
        [Route("Update")]
        public IActionResult UpdateUserAdm([FromBody] UserAdm userAdm)
        {
            if (ModelState.IsValid)
            {
                var userResult = _userAdmservices.UpdateAdmUser(userAdm);
                if (!userResult.Succeeded)
                {
                    List<string> erros = new List<string>();
                    foreach (var erro in userResult.Errors)
                    {
                        erros.Add(erro.Description);
                    }
                    return UnprocessableEntity(erros);
                }
                return Ok("Usuário Atualizado com Sucesso");
            }
            else
            {
                return BadRequest(ModelState);
            }
        }
        #endregion

        #region GetAllUserAdms
        [Authorize(Policy = "Bearer")]
        [HttpGet]
        [Route("getAll")]
        public async Task<IActionResult> GetAllUserAdms()
        {
            var listAdmUsers = await _userAdmservices.GetAllAdm();
             var listAdmUsersDTOS= new List<DTOUserAdmReturn>();

            foreach(var adm in listAdmUsers)
            {
                var admDTO = _mapper.Map<DTOUserAdmReturn>(adm);
                listAdmUsersDTOS.Add(admDTO);
            }             
            return Ok(new JsonResult(listAdmUsersDTOS));   
        }
        #endregion

        #region  DeleteUserAmd
        [Authorize(Policy = "Bearer")]
        [HttpDelete]
        [Route("Update")]
        public IActionResult DeleteUserAmd(string userAdmId)
        {
            var userAdmResult = new IdentityResult();

            var userAdm = _userAdmservices.GetAdmByID(userAdmId);
            if(userAdm is not null)
            {
                var IdentityResult = _userAdmservices.DeleteAdmUser(userAdm);
                return Ok("Sucesso ao deletar Usuário");
            }
            else
            {
                return NotFound("Usuário não encontrado");
            }  
        }
        #endregion
    }
}
