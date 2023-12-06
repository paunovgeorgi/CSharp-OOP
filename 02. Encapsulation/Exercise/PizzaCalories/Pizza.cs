using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Pizza
    {
        private List<Topping> toppings;
        private string name;

        public Pizza()
        {
            toppings = new List<Topping>();
        }
        public string Name
        {
            get => name;
            set
            {
                if (value.Length < 1 || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }
                name = value;
            }
        }
        public Dough Dough { get; set; }

        public int ToppingsCount => toppings.Count;

        public double TotalCalories => Dough.TotalCalories + toppings.Sum(t => t.TotalCalories);


        public void AddTopping(Topping topping)
        {
            if (toppings.Count > 9)
            {
                throw new ArgumentException("Number of toppings should be in range [0..10].");
            }
            toppings.Add(topping);
        }
    }
}
