namespace PizzaCalories
{
    public class Program
    {
        static void Main(string[] args)
        {

            string initialInput;
            Pizza pizza = new();
            
            while ((initialInput = Console.ReadLine()) != "END")
            {
                string[] input = initialInput.Split(" ");

                try
                {
                    switch (input[0])
                    {
                        case "Pizza":
                            pizza.Name = input[1];
                            break;
                        case "Dough":
                            Dough dough = new Dough(input[1].ToLower(), input[2].ToLower(), double.Parse(input[3]));
                            pizza.Dough = dough;
                            break;
                        case "Topping":
                            Topping topping = new(input[1], double.Parse(input[2]));
                            pizza.AddTopping(topping);
                            break;
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    return;
                }
            }

            Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories:f2} Calories.");
        }
    }
}