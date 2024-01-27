using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HighwayToPeak.Core.Contracts;
using HighwayToPeak.Models;
using HighwayToPeak.Models.Contracts;
using HighwayToPeak.Repositories;
using HighwayToPeak.Repositories.Contracts;
using HighwayToPeak.Utilities.Messages;

namespace HighwayToPeak.Core
{
    public class Controller : IController
    {
        private IRepository<IPeak> peaks = new PeakRepository();
        private IRepository<IClimber> climbers = new ClimberRepository();
        private IBaseCamp baseCamp = new BaseCamp();

        public string AddPeak(string name, int elevation, string difficultyLevel)
        {
            if (peaks.Get(name) != null)
            {
                return String.Format(OutputMessages.PeakAlreadyAdded, name);
            }

            if (difficultyLevel != "Extreme" && difficultyLevel != "Hard" && difficultyLevel != "Moderate")
            {
                return String.Format(OutputMessages.PeakDiffucultyLevelInvalid, difficultyLevel);
            }

            IPeak peak = new Peak(name, elevation, difficultyLevel);
            peaks.Add(peak);

            return String.Format(OutputMessages.PeakIsAllowed, name, nameof(PeakRepository));
        }

        public string NewClimberAtCamp(string name, bool isOxygenUsed)
        {
            if (climbers.Get(name) != null)
            {
                return String.Format(OutputMessages.ClimberCannotBeDuplicated, name, nameof(ClimberRepository));
            }

            IClimber climber;

            if (isOxygenUsed)
            {
                climber = new OxygenClimber(name);
            }
            else
            {
                climber = new NaturalClimber(name);
            }

            climbers.Add(climber);
            baseCamp.ArriveAtCamp(climber.Name);
            return String.Format(OutputMessages.ClimberArrivedAtBaseCamp, name);

        }

        public string AttackPeak(string climberName, string peakName)
        {
            if (climbers.Get(climberName) == null)
            {
                return String.Format(OutputMessages.ClimberNotArrivedYet, climberName);
            }

            if (peaks.Get(peakName) == null)
            {
                return String.Format(OutputMessages.PeakIsNotAllowed, peakName);
            }

            if (!baseCamp.Residents.Contains(climberName))
            {
                return String.Format(OutputMessages.ClimberNotFoundForInstructions, climberName, peakName);
            }

            IClimber climber = climbers.Get(climberName);
            IPeak peak = peaks.Get(peakName);

            if (peak.DifficultyLevel == "Extreme" && climber.GetType().Name == nameof(NaturalClimber))
            {
                return String.Format(OutputMessages.NotCorrespondingDifficultyLevel, climberName, peakName);
            }


            baseCamp.LeaveCamp(climberName);
            climber.Climb(peak);

            if (climber.Stamina == 0)
            {
                return String.Format(OutputMessages.NotSuccessfullAttack, climberName);
            }

            baseCamp.ArriveAtCamp(climber.Name);
            return String.Format(OutputMessages.SuccessfulAttack, climberName, peakName);

        }

        public string CampRecovery(string climberName, int daysToRecover)
        {
            if (!baseCamp.Residents.Contains(climberName))
            {
                return String.Format(OutputMessages.ClimberIsNotAtBaseCamp, climberName);
            }

            IClimber climber = climbers.Get(climberName);
            if (climber.Stamina == 10)
            {
                return String.Format(OutputMessages.NoNeedOfRecovery, climberName);
            }

            climber.Rest(daysToRecover);
            return String.Format(OutputMessages.ClimberRecovered, climberName, daysToRecover);
        }

        public string BaseCampReport()
        {
            List<string> baseClimbers = baseCamp.Residents.OrderBy(n=>n).ToList();
            StringBuilder sb = new();
            sb.AppendLine("BaseCamp residents:");
            if (baseClimbers.Any())
            {
                foreach (string climber in baseClimbers)
                {
                    IClimber currentClimber = climbers.Get(climber);
                    sb.AppendLine($"Name: {currentClimber.Name}, Stamina: {currentClimber.Stamina}, Count of Conquered Peaks: {currentClimber.ConqueredPeaks.Count}");
                }
            }
            else
            {
                sb.AppendLine("BaseCamp is currently empty.");
            }

            return sb.ToString().TrimEnd();
        }

        public string OverallStatistics()
        {
            StringBuilder sb = new();
            sb.AppendLine("***Highway-To-Peak***");
            foreach (IClimber climber in climbers.All.OrderByDescending(c=>c.ConqueredPeaks.Count).ThenBy(c=>c.Name))
            {
                sb.AppendLine(climber.ToString());
                if (climber.ConqueredPeaks.Any())
                {
                    List<IPeak> currentPeaks = new List<IPeak>();
                    foreach (string conquered in climber.ConqueredPeaks)
                    {
                        IPeak conqueredPeak = peaks.Get(conquered);
                        if (conqueredPeak != null)
                        {
                            currentPeaks.Add(conqueredPeak);
                        }
                    }

                    foreach (IPeak peak in currentPeaks.OrderByDescending(p => p.Elevation))
                    {
                        sb.AppendLine(peak.ToString());
                    }
                }
            }

            return sb.ToString().TrimEnd();
        }
    }
}
