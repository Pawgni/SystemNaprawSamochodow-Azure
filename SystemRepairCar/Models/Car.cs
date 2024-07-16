using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Http;

namespace SystemRepairCar.Models
{
    public class Car
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Model { get; set; }
        public int Year { get; set; }
        public string? Image { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Type { get; set; }
        public List<string> DamageReports { get; set; } = new List<string>(); 

        [NotMapped]
        [Display(Name = "Upload File")]
        public IFormFile ImageFile { get; set; }

        [NotMapped]
        [Display(Name = "Damage Reports")]
        public List<IFormFile> DamageReportFiles { get; set; }  
    }

    public class ElectricCar : Car
    {
        public int BatteryCapacity { get; set; }
    }

    public class GasolineCar : Car
    {
        public int FuelCapacity { get; set; }
    }
}
