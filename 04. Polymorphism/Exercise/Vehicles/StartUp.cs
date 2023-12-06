using System.Reflection.Metadata.Ecma335;
using Vehicles.Models;
using Vehicles.Models.Interfaces;

namespace Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
         
                IVehicle car = Vehicle("Car");
                IVehicle truck = Vehicle("Truck");
                IVehicle bus = Vehicle("Bus");
                

            int numOfcommands = int.Parse(Console.ReadLine());

                for (int i = 0; i < numOfcommands; i++)
                {
                    string[] tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string command = tokens[0];
                    string vehicle = tokens[1];
                    try
                    {
                        switch (command)
                        {
                            case "Drive":
                                switch (vehicle)
                                {
                                    case "Car":
                                        car.Drive(double.Parse(tokens[2]));
                                        break;
                                    case "Truck":
                                        truck.Drive(double.Parse(tokens[2]));
                                        break;
                                case "Bus":
                                    bus.DriveWithPeople(double.Parse(tokens[2]));
                                    break;
                                }

                                break;
                            case "Refuel":
                                switch (vehicle)
                                {
                                    case "Car":
                                        car.Refuel(double.Parse(tokens[2]));
                                        break;
                                    case "Truck":
                                        truck.Refuel(double.Parse(tokens[2]));
                                        break;
                                }
                                break;
                        case "DriveEmpty":
                            bus.Drive(double.Parse(tokens[2]));
                            break;
                        }
                }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }

                   
                }
                Console.WriteLine($"Car: {car.FuelQuantity:f2}");
                Console.WriteLine($"Truck: {truck.FuelQuantity:f2}");
         
        
        }

        private static IVehicle Vehicle(string type)
        {
            string[] carData = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

            double fuel = double.Parse(carData[1]);
            double consumption = double.Parse(carData[2]);
            double capacity = double.Parse(carData[3]);

            if (fuel > capacity)
            {
                fuel = 0;
            }

            IVehicle vehicle;
            switch (type)
            {
                case "Car":
                    vehicle = new Car(fuel, consumption, capacity);
                    break;
                case "Truck":
                    vehicle = new Truck(fuel, consumption, capacity);
                    break;
                case "Bus":
                    vehicle = new Bus(fuel, consumption, capacity);
                    break;
                default:
                    throw new ArgumentException("Invalid vehicle data");
            }
            return vehicle;
        }
    }
}