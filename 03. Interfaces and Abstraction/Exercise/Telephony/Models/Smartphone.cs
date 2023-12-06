using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Telephony.Models.Interfaces;

namespace Telephony.Models
{
    public class Smartphone : ICaller, IBrowser
    {
        public void Calling(string number)
        {
            Console.WriteLine($"Calling... {number}");
        }

        public void Browsing(string site)
        {
            Console.WriteLine($"Browsing: {site}!");
        }
    }
}
