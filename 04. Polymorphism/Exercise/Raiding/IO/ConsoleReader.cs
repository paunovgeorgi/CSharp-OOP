using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raiding.IO.Interfaces;

namespace Raiding.IO
{
    internal class ConsoleReader : IReader
    {
        public string ReadLine() => Console.ReadLine();

    }
}
