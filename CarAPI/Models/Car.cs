using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarAPI.Models
{
    [Table("Car")]  // Maps to existing SQL "Car" table
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public int Year { get; set; }

        public string EngineSize { get; set; }

        public int BHP { get; set; }

        public DateTime TimeCreated { get; set; }
    }
}
