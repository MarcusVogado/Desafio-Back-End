using Back_End.Models.DTOsModels;
using Back_End.Models.Model;
using Microsoft.AspNetCore.Identity;

namespace Back_End.Repositories.Contracts
{
    public interface IWebTokenServices
    {
        WebToken CreateWebToken(WebToken webToken);

        WebToken? GetWebToken(string refreshToken);

        WebToken GetUserWebToken(string idUser);

        WebToken RefreshToken(string refreshToken);

        bool DeleteWebToken(string refreshToken);

        DTOWebToken BuildTokenAdm(IdentityUser user);

        DTOWebToken BuildTokenAthlete(Athlete athlete); 
    }
}
