using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects.Food
{
    internal class Food : GameObject
    {
        public Food(char drawSymbol, int points) : base(drawSymbol)
        {
            Points = points;

        }

        public int Points { get; private set; }

        //public override bool IsCollideWith(Point point)
        //{
        //    return point.X == X && point.Y == Y;
        //}
    }
}
