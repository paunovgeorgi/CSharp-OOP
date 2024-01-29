using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Food
{
    internal class FoodAsterisk : Food
    {
        private const char Symbol = '*';
        private const int AsteriskPoints = 1;
        public FoodAsterisk() : base(Symbol, AsteriskPoints)
        {
        }

    }
}
