using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Core.Interfaces;
using WildFarm.IO.Interfaces;
using WildFarm.Models;
using WildFarm.Models.Interfaces;

namespace WildFarm.Core
{
    public class Engine : IEngine
    {
        private readonly IReader reader;
        private readonly IWriter writer;
        private readonly ICollection<IAnimal> animals;
        public Engine(IReader reader, IWriter writer)
        {
            this.reader = reader;
            this.writer = writer;
            animals = new List<IAnimal>();
        }

        public void Run()
        {
            int count = 0;
            string input;
            while ((input = Console.ReadLine()) != "End")
            {
                try
                {
                IAnimal animal = CreateAnimal(input);
                string foodInput = Console.ReadLine();
                IFood food = CreateFood(foodInput);
                animals.Add(animal);
                Console.WriteLine(animal.ProduceSound());
                animal.EatFood(food.GetType().Name, food.Quantity);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            foreach (var animal in animals)
            {
                Console.WriteLine(animal.ToString());
            }
        }



        private IAnimal CreateAnimal(string input)
        {
            string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string type = tokens[0];
            switch (type)
            {
                case "Owl": return new Owl(tokens[1], double.Parse(tokens[2]), double.Parse(tokens[3]));
                case "Hen": return new Hen(tokens[1], double.Parse(tokens[2]), double.Parse(tokens[3]));
                case "Mouse": return new Mouse(tokens[1], double.Parse(tokens[2]), tokens[3]);
                case "Dog": return new Dog(tokens[1], double.Parse(tokens[2]), tokens[3]);
                case "Cat": return new Cat(tokens[1], double.Parse(tokens[2]), tokens[3], tokens[4]);
                case "Tiger": return new Tiger(tokens[1], double.Parse(tokens[2]), tokens[3], tokens[4]);
                default:
                    throw new ArgumentException("Invalid animal type");
            }
        }

        private IFood CreateFood(string input)
        {
            string[] tokens = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
            string type = tokens[0];
            switch (type)
            {
                case "Vegetable": return new Vegetable(int.Parse(tokens[1]));
                case "Fruit": return new Fruit(int.Parse(tokens[1]));
                case "Meat": return new Meat(int.Parse(tokens[1]));
                case "Seeds": return new Seeds(int.Parse(tokens[1]));
                default:
                    throw new ArgumentException("Invalid food type");
            }
        }
    }
}
