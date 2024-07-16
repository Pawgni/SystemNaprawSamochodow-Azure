using System;
using System.Threading.Tasks;
using SystemRepairCar.Interfaces;
using SystemRepairCar.Models;

namespace SystemRepairCar.Events
{
    public class CarEventHandler : ICarEventHandler
    {
        private readonly ApplicationDbContext _context;

        public CarEventHandler(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task HandleCarCreatedAsync(Car car)
        {
            var message = new EventMessage
            {
                Message = $"Car created: {car.Name}",
                CreatedAt = DateTime.Now
            };
            _context.EventMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task HandleCarUpdatedAsync(Car car)
        {
            var message = new EventMessage
            {
                Message = $"Car updated: {car.Name}",
                CreatedAt = DateTime.Now
            };
            _context.EventMessages.Add(message);
            await _context.SaveChangesAsync();
        }

        public async Task HandleCarDeletedAsync(int carId)
        {
            var message = new EventMessage
            {
                Message = $"Car deleted: {carId}",
                CreatedAt = DateTime.Now
            };
            _context.EventMessages.Add(message);
            await _context.SaveChangesAsync();
        }
    }
}
