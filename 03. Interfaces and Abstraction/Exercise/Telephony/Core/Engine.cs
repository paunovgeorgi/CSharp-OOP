using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Core.Interfaces;
using Telephony.IO.Interfaces;
using Telephony.Models;

namespace Telephony.Core
{
    public class Engine : IEngine
    {

        private IReader reader;
        private IWriter writer;
        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
        }

        public void Run()
        {
            Smartphone smartphone = new();
            StationaryPhone stationary = new();
            List<string> numbers = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();
            List<string> urls = reader.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries).ToList();

            foreach (string number in numbers)
            {
                if (number.Any(c => !char.IsDigit(c)))
                {
                    writer.WriteLine("Invalid number!");
                    continue;
                }
                if (number.Length == 10)
                {
                    smartphone.Calling(number);
                }
                else if (number.Length == 7)
                {
                    stationary.Calling(number);
                }
            }

            foreach (string url in urls)
            {
                if (url.Any(u => char.IsDigit(u)))
                {
                    writer.WriteLine("Invalid URL!");
                    continue;
                }
                smartphone.Browsing(url);
            }
        }
    }
}
