using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Topping
    {
        public Topping(string type, double weight)
        {
            Type = type;
            Weight = weight;
        }

        private string type;

        private double weight;

        public string Type
        {
            get => type;
           private set
            {
                if (value != "meat" && value != "veggies" && value != "cheese" && value != "sauce" && value != "Meat" && value != "Veggies" && value != "Cheese" && value != "Sauce")
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }
                type = value;
            }
        }

        public double Weight
        {
            get => weight;
            private set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{Type} weight should be in the range [1..50].");
                }
                weight = value;
            }
        }

        public double TotalCalories => CaloriesCount();
        public double CaloriesCount()
        {
            double typeModifier = 1;
            switch (Type)
            {
                case "meat":
                    typeModifier = 1.2;
                    break;
                case "veggies":
                    typeModifier = 0.8;
                    break;
                case "cheese":
                    typeModifier = 1.1;
                    break;
                case "sauce":
                    typeModifier = 0.9;
                    break;
                case "Meat":
                    typeModifier = 1.2;
                    break;
                case "Veggies":
                    typeModifier = 0.8;
                    break;
                case "Cheese":
                    typeModifier = 1.1;
                    break;
                case "Sauce":
                    typeModifier = 0.9;
                    break;
            }

            return  (2 * Weight) * typeModifier;
        }
    }
}
