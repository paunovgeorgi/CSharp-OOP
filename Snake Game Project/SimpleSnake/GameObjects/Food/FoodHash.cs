using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Food
{
    internal class FoodHash : Food
    {
        private const char Symbol = '#';
        private const int HashhPoints = 3;

        public FoodHash() : base(Symbol, HashhPoints)
        {
        }
    }
}
