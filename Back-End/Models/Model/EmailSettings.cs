using System.ComponentModel.DataAnnotations;

namespace Back_End.Models.Model
{
    public class EmailSettings
    {
        [Required]
        public string ServerSmtp { get; set; } = null!;
        [Required]
        public int Port { get; set; }
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string Password { get; set; } = null!;
        [Required]
        public bool EnableSsl { get; set; }
        public string EmailFromAddress { get; set; } = null!;
        public string EmailDestination {get;set; } = null!;
    }
}
