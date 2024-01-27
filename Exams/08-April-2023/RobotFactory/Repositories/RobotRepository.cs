using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RobotService.Models.Contracts;
using RobotService.Repositories.Contracts;

namespace RobotService.Repositories
{
    public class RobotRepository : IRepository<IRobot>
    {
        private readonly List<IRobot> robots = new List<IRobot>();

        public IReadOnlyCollection<IRobot> Models() => robots.AsReadOnly();

        public void AddNew(IRobot model)
        {
            robots.Add(model);
        }

        public bool RemoveByName(string typeName)
        {
            return robots.Remove(robots.FirstOrDefault(r => r.GetType().Name == typeName));
        }

        public IRobot FindByStandard(int interfaceStandard)
        {
            return robots.FirstOrDefault(r => r.InterfaceStandards.Contains(interfaceStandard));
        }
    }
}
