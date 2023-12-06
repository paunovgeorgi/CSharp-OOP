using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Dough
    {
        private string flourType;
        private string bakingTechnique;
        private double weight;

        public Dough(string flourType, string bakingTechnique, double weight)
        {
            FlourType = flourType;
            BakingTechnique = bakingTechnique;
            Weight = weight;
        }

        public string FlourType
        {
            get => flourType;
            private set
            {
                if (value != "white" && value != "wholegrain" && value != "White" && value != "Wholegrain")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                flourType = value;
            }
        }

        public string BakingTechnique
        {
            get => bakingTechnique;
          private set
            {
                if (value != "crispy" && value != "chewy" && value != "homemade" && value != "Crispy" && value != "Chewy" && value != "Homemade")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }
                bakingTechnique = value;
            }
        }

        public double Weight
        {
            get => weight;
           private set
            {
                if (value < 1 && value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }
                weight = value;
            }
        }

        public double TotalCalories => CaloriesCount();
        public double CaloriesCount()
        {
            double typeModifier =1;
            double techniqueModifier = 1;
            switch (FlourType)
            {
                case "white":
                    typeModifier = 1.5;
                    break;
                case "wholegrain":
                    typeModifier = 1.0;
                    break;
            }
            switch (BakingTechnique)
            {
                case "crispy":
                    techniqueModifier = 0.9;
                    break;
                case "chewy":
                    techniqueModifier = 1.1;
                    break;
                case "homemade":
                    techniqueModifier = 1.0;
                    break;
            }

            return (2 * Weight) * techniqueModifier * typeModifier;
        }
    }
}
