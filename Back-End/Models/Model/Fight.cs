using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models.Model
{
    public class Fight
    {
        [Key]
        public Guid Id { get; set; }        
        [Required]
        [ForeignKey("FightKey")]
        public Guid IDFightKey { get; set; }
        [Required]
        [ForeignKey("AthleteOne")]
        public Guid IDAthleteOne { get; set; }
        [Required]
        [ForeignKey("AthleteTwo")]
        public Guid IDAthleteTwo { get; set; }
        [ForeignKey("AthleteWinner")]
        public Guid? IDAthleteWinnerFight { get; set; }=null;
        public Athlete? AthleteOne { get; set; } = null;
        public Athlete? AthleteTwo { get; set; } = null;
        public Athlete? AthleteWinner { get; set; } = null;
        public FightKey? FightKey { get; set; } = null;
    }
}
