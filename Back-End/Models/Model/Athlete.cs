using Back_End.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models.Model
{
   
    public class Athlete
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [EmailAddress]
        public string Email { get; set; }=null!;
        [Required]
        [MinLength(6, ErrorMessage = "Senha precisa ter no mínimo 6 caracteres")]
        public string Password { get; set; } = null!;
        [Required]
        [MinLength(3, ErrorMessage = "Nome precisa ter no mínimo 3 caracteres")]
        public string FullName { get; set; } = null!;
        [Required]
        public DateTime DateOfBirth { get; set; }
        [Required]
        [MinLength(11,ErrorMessage ="Cpf precisa ter no mínimo 11 caracteres") ]
        [MaxLength(11,ErrorMessage =" Cpf pode ter no máximo 11 caracteres")]
        public string CPF { get; set; } = null!;
        [Required]
        public AthleteSex AthleteSex { get; set; }
        [Required]
        [MinLength(3, ErrorMessage = "Nome da equipe precisa ser maior que 3 caracteres")]
        public string Team { get; set; } = null!;
        [Required]
        public AthleteRange AthleteRange { get; set; }
        [Required]
        public AthleteBurden AthleteBurden { get; set; }
        [Required]
        public DateTime RegistrationDate { get; set; }
    }
}
