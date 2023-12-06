
using Raiding.Core.Interfaces;
using Raiding.Factories.Interfaces;
using Raiding.IO.Interfaces;
using Raiding.Models.Interfaces;

namespace Raiding.Core
{
    public class Engine : IEngine
    {
        private IReader reader;
        private IWriter writer;
        private IHeroFactory heroFactory;
        private readonly ICollection<IBaseHero> heroes;
        public Engine(IReader reader, IWriter writer, IHeroFactory heroFactory)
        {
            this.reader = reader;
            this.writer = writer;
            this.heroFactory = heroFactory;
            heroes = new List<IBaseHero>();
        }

        public void Run()
        {
            int numOfHeroes = int.Parse(reader.ReadLine());

            while (heroes.Count < numOfHeroes)
            {
                try
                {
                    heroes.Add(CreateHero());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }

            }

            int BossPower = int.Parse(reader.ReadLine());
            foreach (IBaseHero hero in heroes)
            {
                Console.WriteLine(hero.CastAbility());
            }

            int heroSumPower = heroes.Sum(h => h.Power);

            if (heroSumPower >= BossPower)
            {
                writer.WriteLine("Victory!");
            }
            else
            {
                writer.WriteLine("Defeat...");
            }

        }
        private IBaseHero CreateHero()
        {
            string name = reader.ReadLine();
            string type = reader.ReadLine();
            return heroFactory.Create(type, name);
        }
    }
}
