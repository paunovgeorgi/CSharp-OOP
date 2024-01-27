using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handball.Models.Contracts;
using Handball.Utilities.Messages;

namespace Handball.Models
{
    public class Team : ITeam
    {
        private string name;
        private readonly List<IPlayer> players = new List<IPlayer>();
        private double overallR;
        public Team(string name)
        {
            Name = name;
            PointsEarned = 0;
        }

        public string Name
        {
            get => name;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.TeamNameNull);
                }

                name = value;
            }
        }
        public int PointsEarned { get; private set; }

        public double OverallRating
        {
            get
            {
                if (players.Any())
                {
                    return Math.Round(players.Average(p => p.Rating), 2);
                }
                else
                {
                    return 0;
                }
            }
        }
        
        public IReadOnlyCollection<IPlayer> Players => players.AsReadOnly();
        public void SignContract(IPlayer player)
        {
            players.Add(player);
        }

        public void Win()
        {
            PointsEarned += 3;
            players.ForEach(p=>p.IncreaseRating());
        }

        public void Lose()
        {
            players.ForEach(p=>p.DecreaseRating());
        }

        public void Draw()
        {
            PointsEarned++;
            players.FirstOrDefault(p=>p.GetType().Name == nameof(Goalkeeper)).IncreaseRating();
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Team: {Name} Points: {PointsEarned}");
            sb.AppendLine($"--Overall rating: {OverallRating}");
            if (!players.Any())
            {
                sb.AppendLine("--Players: none");
            }
            else
            {
                sb.AppendLine($"--Players: {string.Join(", ", players.Select(p => p.Name))}");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
