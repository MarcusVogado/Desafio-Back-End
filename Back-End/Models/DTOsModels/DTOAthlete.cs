using Back_End.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.DTOsModels
{
    public class DTOAthlete
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = null!;
        public DateTime DateOfBirth { get; set; }       
        public string CPF { get; set; } = null!;
        public AthleteSex AthleteSex { get; set; }       
        public string Team { get; set; } = null!;
        public AthleteRange AthleteRange { get; set; }
        public AthleteBurden AthleteBurden { get; set; }
        public DateTime RegistrationDate { get; set; }
    }
}
