using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public class Dog : Mammal
    {
        private const double IncreaseRate = 0.40;
        public Dog(string name, double weight, string livingRegion) : base(name, weight, livingRegion)
        {
        }

        public override string ProduceSound()
        {
           return "Woof!";
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

        public override string ToString()
        {
            return base.ToString() + $"{Weight}, {LivingRegion}, {FoodEaten}]";
        }
    }
}
