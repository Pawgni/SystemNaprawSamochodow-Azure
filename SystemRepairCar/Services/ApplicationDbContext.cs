using Microsoft.EntityFrameworkCore;
using SystemRepairCar.Models;

namespace SystemRepairCar
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<ElectricCar> ElectricCars { get; set; }
        public DbSet<GasolineCar> GasolineCars { get; set; }
        public DbSet<EventMessage> EventMessages { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ElectricCar>().ToTable("ElectricCars");
            modelBuilder.Entity<GasolineCar>().ToTable("GasolineCars");
        }
    }
}
