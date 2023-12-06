using System;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {

            string input = string.Empty;
            while ((input = Console.ReadLine()) != "Beast!")
            {
                string[] tokens = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = tokens[0];
                int age = int.Parse(tokens[1]);
                string gender;

                try
                {
                    switch (input)
                    {
                        case "Dog":
                            gender = tokens[2];
                            Dog dog = new(name, age, gender);
                            Print(input, dog);
                            break;
                        case "Cat":
                            gender = tokens[2];
                            Cat cat = new(name, age, gender);
                            Print(input, cat);
                            break;
                        case "Frog":
                            gender = tokens[2];
                            Frog frog = new(name, age, gender);
                            Print(input, frog);
                            break;
                        case "Kitten":
                            Kitten kitten = new(name, age);
                            Print(input, kitten);
                            break;
                        case "Tomcat":
                            Tomcat tomCat = new(name, age);
                            Print(input, tomCat);
                            break;
                    }
                }
                catch (ArgumentException ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }

        }

        static void Print<T>(string input, T type)
        {
            Console.WriteLine(input);
            Console.WriteLine(type);
        }
    }
}
