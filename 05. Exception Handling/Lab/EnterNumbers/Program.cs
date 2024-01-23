namespace EnterNumbers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            int start = 1;
            int end = 100;
            List<int> numbers = new();

            while (numbers.Count < 10)
            {
                string input = Console.ReadLine();
                try
                {
                    if (input.Any(c => !char.IsDigit(c)))
                    {
                        throw new ArgumentException("Invalid Number!");
                    }

                    int currentNum = int.Parse(input);

                    if (currentNum <= start || currentNum >= end)
                    {
                        throw new IndexOutOfRangeException($"Your number is not in range {start} - 100!");
                    }

                    if (currentNum == start + 1)
                    {
                        start = currentNum;
                    }
                    numbers.Add(currentNum);
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

            Console.WriteLine(string.Join(", ", numbers));
        }
    }
}