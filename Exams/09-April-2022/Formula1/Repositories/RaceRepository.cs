﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;

namespace Formula1.Repositories
{
    public class RaceRepository : IRepository<IRace>
    {
        private readonly List<IRace> races = new List<IRace>();
        public IReadOnlyCollection<IRace> Models => races.AsReadOnly();
        public void Add(IRace model)
        {
           races.Add(model);
        }

        public bool Remove(IRace model)
        {
            return races.Remove(model);
        }

        public IRace FindByName(string name)
        {
            return races.FirstOrDefault(r => r.RaceName == name);
        }
    }
}
