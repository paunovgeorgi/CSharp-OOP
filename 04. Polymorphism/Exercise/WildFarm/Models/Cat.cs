using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WildFarm.Models
{
    public class Cat : Feline
    {
        private const double IncreaseRate = 0.30;
        public Cat(string name, double weight, string livingRegion, string breed) : base(name, weight, livingRegion, breed)
        {
        }

        public override string ProduceSound()
        {
            return "Meow";
        }

        public override void EatFood(string type, int quantity)
        {
            if (type != "Meat" && type != "Vegetable")
            {
                throw new ArgumentException($"{GetType().Name} does not eat {type}!");
            }
            Weight += quantity * IncreaseRate;
            FoodEaten += quantity;
        }
    }
}
