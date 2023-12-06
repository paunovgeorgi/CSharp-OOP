using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public class Hen : Bird
    {
        public Hen(string name, double weight, double wingSize) : base(name, weight, wingSize)
        {
        }

        private const double IncreaseRate = 0.35;

        public override string ProduceSound()
        {
            return "Cluck";
        }

        public override void EatFood(string type, int quantity)
        {
            Weight += quantity * IncreaseRate;
            FoodEaten += quantity;
        }
    }
}
