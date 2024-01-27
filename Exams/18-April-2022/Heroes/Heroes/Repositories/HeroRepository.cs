﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Heroes.Models.Contracts;
using Heroes.Repositories.Contracts;

namespace Heroes.Repositories
{
    public class HeroRepository : IRepository<IHero>
    {
        private readonly List<IHero> heroes = new List<IHero>();
        public IReadOnlyCollection<IHero> Models => heroes.AsReadOnly();
        public void Add(IHero model)
        {
            heroes.Add(model);
        }

        public bool Remove(IHero model)
        {
            return heroes.Remove(model);
        }

        public IHero FindByName(string name)
        {
            return heroes.FirstOrDefault(h => h.Name == name);
        }
    }
}
