using Back_End.Models.Enums;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models.Model
{
    
    public class UserAdm : IdentityUser
    {
        [Required]
        public string FullName { get; set; } = null!;
        [Required]
        public UserAdmType UserAdmType { get; set; }    
    }
}
