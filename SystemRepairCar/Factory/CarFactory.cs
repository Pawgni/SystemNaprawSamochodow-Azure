using System;
using SystemRepairCar.Interfaces;
using SystemRepairCar.Models;

namespace SystemRepairCar.Factories
{
    public class CarFactory : ICarFactory
    {
        public Car CreateCar(string type, string name, string model, int year, string image)
        {
            switch (type.ToLower())
            {
                case "electric":
                    return new ElectricCar
                    {
                        Name = name,
                        Model = model,
                        Year = year,
                        Image = image,
                        CreatedAt = DateTime.Now,
                        BatteryCapacity = 100 
                    };
                case "gasoline":
                    return new GasolineCar
                    {
                        Name = name,
                        Model = model,
                        Year = year,
                        Image = image,
                        CreatedAt = DateTime.Now,
                        FuelCapacity = 50
                    };
                default:
                    throw new ArgumentException("Invalid car type");
            }
        }
    }
}
