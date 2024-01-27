using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories.Contracts;

namespace NavalVessels.Repositories
{
    public class VesselRepository : IRepository<IVessel>
    {
        private readonly List<IVessel> vessels = new List<IVessel>();
        public IReadOnlyCollection<IVessel> Models => vessels;
        public void Add(IVessel model)
        {
            vessels.Add(model);
        }

        public bool Remove(IVessel model)
        {
            return vessels.Remove(model);
        }

        public IVessel FindByName(string name)
        {
            return vessels.FirstOrDefault(v => v.Name == name);
        }
    }
}
