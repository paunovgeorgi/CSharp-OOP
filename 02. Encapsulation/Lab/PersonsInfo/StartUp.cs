namespace PersonsInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {

            Team team = new Team("SoftUni");

            int numOfPeople = int.Parse(Console.ReadLine());
            List<Person> people = new();

            for (int i = 0; i < numOfPeople; i++)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string first = input[0];
                string last = input[1];
                int age = int.Parse(input[2]);
                decimal salary = decimal.Parse(input[3]);

                try
                {
                    people.Add(new Person(first, last, age, salary));
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }

            }

            foreach (Person person in people)
            {
                team.AddPlayer(person);
            }

            Console.WriteLine($"First team has {team.FirstTeam.Count} players.");
            Console.WriteLine($"Reserve team has {team.ReserveTeam.Count} players.");
        }
    }
}