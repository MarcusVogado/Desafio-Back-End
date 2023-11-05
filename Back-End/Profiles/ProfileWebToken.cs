using AutoMapper;
using Back_End.Models.DTOsModels;
using Back_End.Models.Model;

namespace Back_End.Profiles
{
    public class ProfileWebToken :Profile
    {
        public ProfileWebToken()
        {
            CreateMap<WebToken, DTOWebToken>();
        }
    }
}
