using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Vehicles.Models.Interfaces;

namespace Vehicles.Models
{
    public class Bus : IVehicle
    {
        public Bus(double fuelQuantity, double fuelConsumption, double tankCapacity)
        {
            FuelQuantity = fuelQuantity;
            FuelConsumption = fuelConsumption;
            TankCapacity = tankCapacity;
        }

        public double FuelQuantity { get; private set; }
        public double FuelConsumption { get; private set; }
        public double TankCapacity { get; private set; }
        public void DriveWithPeople(double km)
        {

            if (FuelQuantity >= km * (FuelConsumption + 1.4))
                {
                    FuelQuantity -= km * (FuelConsumption + 1.4);
                    Console.WriteLine($"Car travelled {km} km");
                }
                else
                {
                    Console.WriteLine("Car needs refueling");
                }
        }

        private bool isEmpty = false;
        public void Drive(double km, bool isEmpty = true)
        {
            if (FuelQuantity >= km * (FuelConsumption))
            {
                FuelQuantity -= km * (FuelConsumption);
                Console.WriteLine($"Car travelled {km} km");
            }
            else
            {
                Console.WriteLine("Car needs refueling");
            }
        }

        public void Refuel(double liters)
        {
            if (liters <= 0)
            {
                throw new ArgumentException("Fuel must be a positive number");
            }
            if (FuelQuantity + (liters * 0.95) <= TankCapacity)
            {
                FuelQuantity += (liters * 0.95);
            }
            else
            {
                Console.WriteLine($"Cannot fit {liters} fuel in the tank");
            }
        }
    }
}
