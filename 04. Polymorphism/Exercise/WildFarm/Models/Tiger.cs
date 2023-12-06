using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public class Tiger : Feline
    {
        private const double IncreaseRate = 1.00;
        public Tiger(string name, double weight, string livingRegion, string breed) : base(name, weight,  livingRegion, breed)
        {
        }

        public override string ProduceSound()
        {
            return "ROAR!!!";
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
