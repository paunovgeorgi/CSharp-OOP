using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class PlanetRepository : IRepository<IPlanet>
    {
        private readonly List<IPlanet> planets = new List<IPlanet>();
        public IReadOnlyCollection<IPlanet> Models => planets.AsReadOnly();
        public void Add(IPlanet model)
        {
            planets.Add(model);
        }

        public bool Remove(IPlanet model)
        {
            return planets.Remove(model);
        }

        public IPlanet FindByName(string name)
        {
            return planets.FirstOrDefault(p => p.Name == name);
        }
    }
}
