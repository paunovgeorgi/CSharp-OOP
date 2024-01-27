using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handball.Models.Contracts;
using Handball.Repositories.Contracts;

namespace Handball.Repositories
{
    public class PlayerRepository : IRepository<IPlayer>
    {
        private readonly List<IPlayer> players = new List<IPlayer>();
        public IReadOnlyCollection<IPlayer> Models => players;
        public void AddModel(IPlayer model)
        {
            players.Add(model);
        }

        public bool RemoveModel(string name)
        {
            return players.Remove(players.FirstOrDefault(p => p.Name == name));
        }

        public bool ExistsModel(string name)
        {
            return players.Any(p => p.Name == name);
        }

        public IPlayer GetModel(string name)
        {
            return players.FirstOrDefault(p => p.Name == name);
        }
    }
}
