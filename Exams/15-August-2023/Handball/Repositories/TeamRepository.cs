using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handball.Models.Contracts;
using Handball.Repositories.Contracts;

namespace Handball.Repositories
{
    public class TeamRepository : IRepository<ITeam>
    {
        private readonly List<ITeam> teams = new List<ITeam>();
        public IReadOnlyCollection<ITeam> Models => teams;
        public void AddModel(ITeam model)
        {
            teams.Add(model);
        }

        public bool RemoveModel(string name)
        {
            return teams.Remove(teams.FirstOrDefault(t => t.Name == name));
        }

        public bool ExistsModel(string name)
        {
            return teams.Any(t => t.Name == name);
        }

        public ITeam GetModel(string name)
        {
            return teams.FirstOrDefault(t => t.Name == name);
        }
    }
}
