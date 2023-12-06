using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Vehicles.Models.Interfaces
{
    public interface IVehicle
    {
        public double FuelQuantity { get; }
        public double FuelConsumption { get; }

        public double TankCapacity { get; }

        void Drive(double km);
        void Refuel(double liters);

        virtual void DriveWithPeople(double km)
        {

        }
    }
}
