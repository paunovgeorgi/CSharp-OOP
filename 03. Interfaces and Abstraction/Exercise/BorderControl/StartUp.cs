using BorderControl.Models;
using BorderControl.Models.Interfaces;

namespace BorderControl
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBuyer> entries = new();

            int numOfPeople = int.Parse(Console.ReadLine());

            for (int i = 0; i < numOfPeople; i++)
            {
                string[] tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string type = tokens[0];

                if (tokens.Length == 4)
                {
                    entries.Add(new Citizen(tokens[0], int.Parse(tokens[1]), tokens[2], tokens[3]));
                }
                else
                {
                    entries.Add(new Rebel(tokens[0], int.Parse(tokens[1]), tokens[2]));
                }
            }

            string input;

            while ((input = Console.ReadLine()) != "End")
            {
                if (entries.Any(e=>e.Name == input))
                {
                    entries.FirstOrDefault(e=>e.Name == input).BuyFood();
                }
            }

            Console.WriteLine(entries.Sum(e=>e.Food));
        }
    }
}