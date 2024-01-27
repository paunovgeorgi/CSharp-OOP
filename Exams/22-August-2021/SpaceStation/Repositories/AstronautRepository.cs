﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Repositories.Contracts;

namespace SpaceStation.Repositories
{
    public class AstronautRepository : IRepository<IAstronaut>
    {
        private readonly List<IAstronaut> astronauts = new List<IAstronaut>();
        public IReadOnlyCollection<IAstronaut> Models => astronauts.AsReadOnly();
        public void Add(IAstronaut model)
        {
            astronauts.Add(model);
        }

        public bool Remove(IAstronaut model)
        {
            return astronauts.Remove(model);
        }

        public IAstronaut FindByName(string name)
        {
            return astronauts.FirstOrDefault(a => a.Name == name);
        }
    }
}
