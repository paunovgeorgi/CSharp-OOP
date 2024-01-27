using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Formula1.Models.Contracts;
using Formula1.Repositories.Contracts;

namespace Formula1.Repositories
{
    public class PilotRepository : IRepository<IPilot>
    {
        private readonly List<IPilot> pilots = new List<IPilot>();
        public IReadOnlyCollection<IPilot> Models => pilots;
        public void Add(IPilot model)
        {
            pilots.Add(model);
        }

        public bool Remove(IPilot model)
        {
            return pilots.Remove(model);
        }

        public IPilot FindByName(string name)
        {
            return pilots.FirstOrDefault(p => p.FullName == name);
        }
    }
}
