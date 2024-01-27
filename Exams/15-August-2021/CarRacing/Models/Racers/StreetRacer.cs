using System;
using System.Collections.Generic;
using System.Text;
using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class StreetRacer : Racer
    {
        private const int StreetDrivingExperience = 10;
        private const string StreetRacingBehavior = "aggressive";
        public StreetRacer(string username, ICar car) : base(username, StreetRacingBehavior, StreetDrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            DrivingExperience += 5;
        }
    }
}
