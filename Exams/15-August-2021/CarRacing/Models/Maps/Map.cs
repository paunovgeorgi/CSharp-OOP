using System;
using System.Collections.Generic;
using System.Text;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Models.Maps
{
    public class Map :IMap
    {
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            IRacer winner;
            IRacer loser;
            double racerOneMultiplier = racerOne.RacingBehavior == "strict" ? 1.2 :1.1;
            double racerTwoMultiplier = racerTwo.RacingBehavior == "strict" ? 1.2 :1.1;
            double racerOneTotalScore = racerOne.Car.HorsePower * racerOne.DrivingExperience * racerOneMultiplier;
            double racerTwoTotalScore = racerTwo.Car.HorsePower * racerTwo.DrivingExperience * racerTwoMultiplier;

            if (!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return OutputMessages.RaceCannotBeCompleted;
            }
            else if (!racerOne.IsAvailable())
            {
                winner = racerTwo;
                loser = racerOne;
                return String.Format(OutputMessages.OneRacerIsNotAvailable, winner.Username, loser.Username);
            }
            else if (!racerTwo.IsAvailable())
            {
                winner = racerOne;
                loser = racerTwo;
                return String.Format(OutputMessages.OneRacerIsNotAvailable, winner.Username, loser.Username);
            }
            else
            {
                racerOne.Race();
                racerTwo.Race();
                if (racerOneTotalScore > racerTwoTotalScore)
                {
                    winner = racerOne;
                }
                else
                {
                    winner = racerTwo;
                }
            }

            return String.Format(OutputMessages.RacerWinsRace, racerOne.Username, racerTwo.Username, winner.Username);
        }
    }
}
