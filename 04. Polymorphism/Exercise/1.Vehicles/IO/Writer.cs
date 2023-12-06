using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _1.Vehicles.IO.Interfaces;

namespace _1.Vehicles.IO
{
    public class Writer : IWriter
    {
        public void WriteLine(string line) => Console.WriteLine(line);

    }
}
