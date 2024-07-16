using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace SystemRepairCar.Models
{
    public class CarDto
    {
        [Required]
        public string Type { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public int Year { get; set; }
        public IFormFile ImageFile { get; set; }
    }
}
