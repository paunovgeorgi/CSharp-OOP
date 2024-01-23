using System.Globalization;

namespace SumofIntegers
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
            int sum = 0;

            foreach (var element in input)
            {
                try
                {
                    if (int.TryParse(element, out int result))
                    {
                        sum += result;
                    }
                    else if (long.TryParse(element, out long resultLong))
                    {
                  
                            throw new IndexOutOfRangeException($"The element '{element}' is out of range!");
                        
                    }
                    else
                    {
                        throw new FormatException($"The element '{element}' is in wrong format!");
                    }
                }
                catch (IndexOutOfRangeException ex)
                {
                    Console.WriteLine(ex.Message);

                }
                catch (FormatException ex)
                {
                    Console.WriteLine(ex.Message);
                }
                finally
                {
                    Console.WriteLine($"Element '{element}' processed - current sum: {sum}");
                }
            }

            Console.WriteLine($"The total sum of all integers is: {sum}");
        }
    }
}