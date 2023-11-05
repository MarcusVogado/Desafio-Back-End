using Back_End.Models.Model;
using Microsoft.AspNetCore.Identity;

namespace Back_End.Repositories.Contracts
{
    public interface IUserAdmServices
    {
        IdentityResult CreateAdmUser(UserAdm userAdm,string password);
        IdentityResult UpdateAdmUser(UserAdm athlete);
        IdentityResult DeleteAdmUser(UserAdm userAdm);
        UserAdm GetAdmUser(string email, string password);
        UserAdm GetAdmByID(string id);
        Task<List<UserAdm>> GetAllAdm();
    }
}
