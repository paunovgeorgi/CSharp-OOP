using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public class Owl : Bird
    {
        private const double IncreaseRate = 0.25;
        public Owl(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        public override string ProduceSound()
        {
            return "Hoot Hoot";
        }

        public override void EatFood(string type, int quantity)
        {
            if (type != "Meat")
            {
                throw new ArgumentException($"{GetType().Name} does not eat {type}!");
            }
            Weight += quantity * IncreaseRate;
            FoodEaten += quantity;
        }
    }
}
