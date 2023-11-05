using Back_End.Models.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Back_End.Models.Model
{
    
    public class Championship
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(60, ErrorMessage = "Maximo 60 caracteres")]
        public string TitleChampionship { get; set; } = null!; 

        [Required]
        public string City { get; set; } = null!;
        [Required]
        public string State { get; set; } = null!;
        [Required]
        public DateTime DateOfRealization { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Maximo 100 caracteres")]
        public string Gymnasium { get; set; } = null!;
        [Required]
        [MinLength(20, ErrorMessage = "Minimo 20 caracteres")]
        public string GeneralInformation { get; set; } = null!;
        public string ?PublicEntrance { get; set; } = null!;
        public string? ImagePath { get; set; } 
        [Required]
        public ChampionshipType TypeOfChampionship { get; set; }
        [Required]
        public ChampionshipStage StageOfChampionship { get; set; }
        [Required]
        public bool Status { get; set; }
        [NotMapped]
        public IFormFile ImageFile { get; set; } = null!;
    }
}
