using _1.Vehicles.Core;
using _1.Vehicles.Core.Interfaces;
using _1.Vehicles.Factories;
using _1.Vehicles.IO;

namespace _1.Vehicles
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine(new Reader(), new Writer(), new VehicleFactory());
            engine.Run();
        }
    }
}