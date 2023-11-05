using Back_End.Models.Model;
using Back_End.Repositories.Contracts;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Back_End.Repositories.Services
{
    public class UserAdmServices : IUserAdmServices
    {
        private readonly UserManager<UserAdm> _userManager;
        

        public UserAdmServices(UserManager<UserAdm> userManager)
        {
            _userManager = userManager;
        }
        public IdentityResult CreateAdmUser(UserAdm user, string password)
        {
            var resultCreateUser = _userManager.CreateAsync(user, password);
            return resultCreateUser.Result;
        }

        public IdentityResult UpdateAdmUser(UserAdm user)
        {
            var userResult = new IdentityResult();
            var userUpdate = GetAdmByID(user.Id);
            if (userUpdate is not null)
            {
                userUpdate.Email = user.Email;
                userUpdate.FullName = user.FullName;
                userUpdate.PasswordHash = user.PasswordHash;
                userResult = _userManager.UpdateAsync(userUpdate).Result;
                return userResult;

            }
            else
            {
                return userResult = _userManager.UpdateAsync(user).Result;
            }
        }

        public UserAdm GetAdmUser(string email, string password)
        {
            var user = _userManager.FindByEmailAsync(email).Result;
            if (_userManager.CheckPasswordAsync(user, password).Result)
            {
                return user;
            }
            else
            {
                return null;
            }
        }
        public async Task<List<UserAdm>> GetAllAdm()
        {
            
            return await _userManager.Users.ToListAsync();
        }

        public UserAdm GetAdmByID(string id)
        {
            return _userManager.FindByIdAsync(id).Result;

        }

        public IdentityResult DeleteAdmUser(UserAdm userAdm)
        {
            var result = new IdentityResult();

            UserAdm? userRemove = GetAdmByID(userAdm.Id);
            if (userRemove is not null)
            {
              return result =  _userManager.DeleteAsync(userRemove).Result;

            }
            else
            {
                return _userManager.DeleteAsync(userRemove).Result;

            }           
        }
    }
}
