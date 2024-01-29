using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Food
{
    internal class FoodDollar : Food
    {
        private const char Symbol = '$';
        private const int DollarPoints = 2;

        public FoodDollar() : base(Symbol, DollarPoints)
        {
        }
    }
}
