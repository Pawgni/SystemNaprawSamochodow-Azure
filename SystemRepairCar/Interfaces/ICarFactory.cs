using SystemRepairCar.Models;

namespace SystemRepairCar.Interfaces
{
    public interface ICarFactory
    {
        Car CreateCar(string type, string name, string model, int year, string image);
    }
}
