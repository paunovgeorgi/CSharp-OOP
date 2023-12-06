using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl.Models.Interfaces
{
    public interface IBuyer
    {
        void BuyFood();
        public string Name { get; }
        public int Food { get; }
    }
}
