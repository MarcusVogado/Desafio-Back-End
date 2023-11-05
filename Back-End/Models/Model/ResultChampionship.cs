using Back_End.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models.Model
{
    public class ResultChampionship
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [ForeignKey("Championship")]
        public Guid IDChampionship { get; set; }
        public Championship Championship { get; set; } = null!;   
    }
}
