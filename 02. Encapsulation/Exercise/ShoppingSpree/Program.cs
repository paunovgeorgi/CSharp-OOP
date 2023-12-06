namespace ShoppingSpree
{
    public class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new();
            List<Product> products = new();
            string[] initialPersonInput = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);
            foreach (string s in initialPersonInput)
            {
                string[] personData = s.Split('=', StringSplitOptions.RemoveEmptyEntries);
                try
                {
                people.Add(new Person(personData[0], decimal.Parse(personData[1])));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }


            string[] productInput = Console.ReadLine().Split(';', StringSplitOptions.RemoveEmptyEntries);

            foreach (string s in productInput)
            {
                string[] productData = s.Split('=', StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    products.Add(new Product(productData[0], decimal.Parse(productData[1])));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
            }

            string input;
            while ((input = Console.ReadLine()) != "END")
            {
                string[] personProductData = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string personName = personProductData[0];
                string productName = personProductData[1];

                Person person = people.FirstOrDefault(p => p.Name == personName);
                Product product = products.FirstOrDefault(p => p.Name == productName);

                if (person is not null && product is not null)
                {
                person.AddProduct(product);
                }
            }

            foreach (Person person in people)
            {
                Console.WriteLine(person);
            }
        }
    }
}