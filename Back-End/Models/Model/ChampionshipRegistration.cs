using Back_End.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models.Model
{
   
    public class ChampionshipRegistration
    {
        [Key]
        public Guid Id { get; set; }
        public ChampionshipRegistrationStatus ChampionshipRegistrationStatus { get; set; }
        [Required]
        public DateTime DateOfRegistration { get; set; }
        [Required]
        [ForeignKey("Athlete")]
        public Guid IDAthlete { get; set; }
        public Athlete Athlete { get; set; } = null!;
        [Required]
        [ForeignKey("FightKey")]
        public Guid IDFightKey { get; set; } 
        public FightKey FightKey { get; set; }= null!;
    }
}
