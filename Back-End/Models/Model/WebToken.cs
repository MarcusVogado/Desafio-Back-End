using Microsoft.AspNetCore.Identity;
using Microsoft.Identity.Client;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models.Model
{
    [Table("WebTokens")]
    public class WebToken
    {
        [Key]
        public int ID { get; set; }
        public string Token { get; set; } = null!;
        public string RefreshToken { get; set; } = null!;
        public DateTime Criado { get; set; }
        public DateTime Atualizado { get; set; }
        public DateTime ExpirationToken { get; set; }
        public DateTime ExpirationRefreshToken { get; set; }

        [Required]        
        public string IDUser { get; set; } = null!;            
    }
}
