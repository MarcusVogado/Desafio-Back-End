using Back_End.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Permissions;

namespace Back_End.Models.Model
{
    
    public class FightKey
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        public AthleteRange AthleteRange { get; set; }
        [Required]
        public AthleteBurden AthleteBurden { get; set; }
        [Required]
        public AthleteSex AthleteSex { get; set;}
        [Required]
        public FigthKeyStatus Status { get; set; }
        [Required]
        [ForeignKey("Championship")]
        public Guid IDChampionship { get; set; }
        public Championship? Championship { get; set;}
    }
}
