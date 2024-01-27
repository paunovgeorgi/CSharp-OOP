using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handball.Models
{
    public class Goalkeeper : Player
    {
        private const double InitialRating = 2.5;
        private const double Increase = 0.75;
        private const double Decrease = 1.25;
        public Goalkeeper(string name) : base(name, InitialRating)
        {
        }

        public override void IncreaseRating()
        {
            if (Rating + Increase > 10)
            {
                Rating = 10;
            }
            else
            {
                Rating += Increase;
            }
        }

        public override void DecreaseRating()
        {
            if (Rating - Decrease < 1)
            {
                Rating = 1;
            }
            else
            {
                Rating -= Decrease;
            }
        }
    }
}
