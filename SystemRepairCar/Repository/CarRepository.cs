using Microsoft.EntityFrameworkCore;
using SystemRepairCar.Models;

namespace SystemRepairCar.Repository
{
    public class CarRepository : ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<IEnumerable<Car>> GetAllCarsAsync()
        {
            return await _context.Cars.ToListAsync();
        }

        public async Task<Car> GetCarByIdAsync(int id)
        {
            return await _context.Cars.FirstOrDefaultAsync(m => m.Id == id);
        }

        public async Task AddCarAsync(Car car)
        {
            _context.Add(car);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateCarAsync(Car car)
        {
            var existingCar = await GetCarByIdAsync(car.Id);
            if (existingCar != null)
            {
                _context.Entry(existingCar).State = EntityState.Detached;
            }

            _context.Update(car);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteCarAsync(int id)
        {
            var car = await _context.Cars.FindAsync(id);
            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> CarExistsAsync(int id)
        {
            return await _context.Cars.AnyAsync(e => e.Id == id);
        }
    }
}
