using System;
using System.Linq;
using System.Text;
using Handball.Core.Contracts;
using Handball.Models;
using Handball.Models.Contracts;
using Handball.Repositories;
using Handball.Repositories.Contracts;
using Handball.Utilities.Messages;

namespace Handball.Core
{
    public class Controller : IController
    {
        private IRepository<IPlayer> players = new PlayerRepository();
        private IRepository<ITeam> teams = new TeamRepository();
        public string NewTeam(string name)
        {
            if (teams.ExistsModel(name))
            {
                return String.Format(OutputMessages.TeamAlreadyExists,name, nameof(TeamRepository));
            }

            ITeam team = new Team(name);
            teams.AddModel(team);

            return String.Format(OutputMessages.TeamSuccessfullyAdded, name, nameof(TeamRepository));
        }

        public string NewPlayer(string typeName, string name)
        {
            if (typeName != nameof(CenterBack) && typeName != nameof(ForwardWing) && typeName != nameof(Goalkeeper))
            {
                return String.Format(OutputMessages.InvalidTypeOfPosition, typeName);
            }

            if (players.ExistsModel(name))
            {
                return String.Format(OutputMessages.PlayerIsAlreadyAdded, name, nameof(PlayerRepository), players.GetModel(name).GetType().Name);
            }

            IPlayer player;
            if (typeName == nameof(CenterBack))
            {
                player = new CenterBack(name);
            }
            else if (typeName == nameof(ForwardWing))
            {
                player = new ForwardWing(name);
            }
            else
            {
                player = new Goalkeeper(name);
            }
            players.AddModel(player);

            return String.Format(OutputMessages.PlayerAddedSuccessfully, name);
        }

        public string NewContract(string playerName, string teamName)
        {
            if (!players.ExistsModel(playerName))
            {
                return String.Format(OutputMessages.PlayerNotExisting, playerName, nameof(PlayerRepository));
            }

            if (!teams.ExistsModel(teamName))
            {
                return String.Format(OutputMessages.TeamNotExisting, teamName, nameof(TeamRepository));
            }

            IPlayer player = players.GetModel(playerName);
            if (player.Team != null)
            {
                return String.Format(OutputMessages.PlayerAlreadySignedContract, playerName, player.Team);
            }
            player.JoinTeam(teamName);

            ITeam team = teams.GetModel(teamName);
            team.SignContract(player);

            return String.Format(OutputMessages.SignContract, playerName, teamName);
        }

        public string NewGame(string firstTeamName, string secondTeamName)
        {
            ITeam firstTeam = teams.GetModel(firstTeamName);
            ITeam secondTeam = teams.GetModel(secondTeamName);

            double firstTeamRating = firstTeam.OverallRating;
            double secondTeamRating = secondTeam.OverallRating;

            if (firstTeamRating != secondTeamRating)
            {
                ITeam winner = null;
                ITeam loser = null;

                if (firstTeamRating > secondTeamRating)
                {
                    winner = firstTeam;
                    loser = secondTeam;
                }
                else if (secondTeamRating > firstTeamRating)
                {
                    winner = secondTeam;
                    loser = firstTeam;
                }

                winner.Win();
                loser.Lose();
                return String.Format(OutputMessages.GameHasWinner, winner.Name, loser.Name);
            }

            firstTeam.Draw();
            secondTeam.Draw();
            return String.Format(OutputMessages.GameIsDraw, firstTeamName, secondTeamName);
        }

        public string PlayerStatistics(string teamName)
        {
            ITeam team = teams.GetModel(teamName);
            StringBuilder sb = new();

            sb.AppendLine($"***{teamName}***");
            foreach (IPlayer player in team.Players.OrderByDescending(p=>p.Rating).ThenBy(p=>p.Name))
            {
                sb.AppendLine(player.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string LeagueStandings()
        {
            StringBuilder sb = new();

            sb.AppendLine("***League Standings***");
            foreach (ITeam team in teams.Models.OrderByDescending(t => t.PointsEarned).ThenByDescending(t => t.OverallRating).ThenBy(t => t.Name))
            {
                sb.AppendLine(team.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}


