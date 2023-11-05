using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.DTOsModels
{
    public class DTOUserLogin
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        [MinLength(6,ErrorMessage ="A senha deve conter no mínimo 6 digitos")]
        public string Password { get; set; } = null!;
    }
}
