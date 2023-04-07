using System.ComponentModel.DataAnnotations;

namespace CarAPI.Models
{
    public class CarDTO
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Brand { get; set; }

        [Required]
        [MaxLength(30)]
        public string Model { get; set; }

        [Required]
        public int Year { get; set; }

        [Required]
        [MaxLength(7)]
        public string EngineSize { get; set; }

        [Required]
        public int BHP { get; set; }
    }
}
