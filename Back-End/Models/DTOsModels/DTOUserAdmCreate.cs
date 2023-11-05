using Back_End.Models.Enums;
using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.DTOsModels
{
    public class DTOUserAdmCreate
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public UserAdmType UserAdmType { get; set; }    
    }
}
