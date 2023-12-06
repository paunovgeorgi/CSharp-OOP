using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using _1.Vehicles.Factories.Interfaces;
using _1.Vehicles.Models;
using _1.Vehicles.Models.Interfaces;

namespace _1.Vehicles.Factories
{
    public class VehicleFactory : IVehicleFactory
    {
        public IVehicle Create(string type, double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            if (fuelQuantity > tankCapacity)
            {
                fuelQuantity = 0;
            }
            switch (type)
            {
                case "Car": return new Car(fuelQuantity, fuelConsumption, tankCapacity);
                case "Truck": return new Truck(fuelQuantity, fuelConsumption, tankCapacity);
                case "Bus": return new Bus(fuelQuantity, fuelConsumption, tankCapacity);
                default:
                    throw new ArgumentException("Invalid vehicle type");
            }
        }
    }
}
