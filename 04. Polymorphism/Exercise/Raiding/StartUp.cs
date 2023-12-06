using Raiding.Core;
using Raiding.Core.Interfaces;
using Raiding.Factories;
using Raiding.IO;

namespace Raiding
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine(new ConsoleReader(), new ConsoleWriter(), new HeroFactory());
            engine.Run();
        }
    }
}