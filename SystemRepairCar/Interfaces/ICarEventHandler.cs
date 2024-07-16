using System.Threading.Tasks;
using SystemRepairCar.Models;

namespace SystemRepairCar.Interfaces
{
    public interface ICarEventHandler
    {
        Task HandleCarCreatedAsync(Car car);
        Task HandleCarUpdatedAsync(Car car);
        Task HandleCarDeletedAsync(int carId);
    }
}
