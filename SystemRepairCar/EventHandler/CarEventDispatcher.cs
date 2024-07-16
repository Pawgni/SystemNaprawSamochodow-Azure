using System.Collections.Generic;
using System.Threading.Tasks;
using SystemRepairCar.Interfaces;
using SystemRepairCar.Models;

namespace SystemRepairCar.Events
{
    public class CarEventDispatcher
    {
        private readonly IEnumerable<ICarEventHandler> _eventHandlers;

        public CarEventDispatcher(IEnumerable<ICarEventHandler> eventHandlers)
        {
            _eventHandlers = eventHandlers;
        }

        public async Task CarCreatedAsync(Car car)
        {
            foreach (var handler in _eventHandlers)
            {
                await handler.HandleCarCreatedAsync(car);
            }
        }

        public async Task CarUpdatedAsync(Car car)
        {
            foreach (var handler in _eventHandlers)
            {
                await handler.HandleCarUpdatedAsync(car);
            }
        }

        public async Task CarDeletedAsync(int carId)
        {
            foreach (var handler in _eventHandlers)
            {
                await handler.HandleCarDeletedAsync(carId);
            }
        }
    }
}
