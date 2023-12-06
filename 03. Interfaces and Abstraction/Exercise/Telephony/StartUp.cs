using Telephony.Core;
using Telephony.Core.Interfaces;
using Telephony.IO;
using Telephony.Models;

namespace Telephony
{
    internal class StartUp
    {
        static void Main(string[] args)
        {
            IEngine engine = new Engine(new ConsoleReader(), new FileWriter());

            engine.Run();
        }
    }
}