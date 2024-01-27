﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories.Contracts;

namespace HighwayToPeak.Repositories
{
    public class PeakRepository :IRepository<IPeak>
    {
        private readonly List<IPeak> peaks = new List<IPeak>();
        public IReadOnlyCollection<IPeak> All => peaks;
        public void Add(IPeak model)
        {
            peaks.Add(model);
        }

        public IPeak Get(string name)
        {
            return peaks.FirstOrDefault(p => p.Name == name);
        }
    }
}
