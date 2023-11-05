using AutoMapper;
using Back_End.Models.DTOsModels;
using Back_End.Models.Model;

namespace Back_End.Profiles
{
    public class ProfileAthlete : Profile
    {
        public ProfileAthlete()
        {
            CreateMap<Athlete, DTOAthlete>();
        }
    }
}
