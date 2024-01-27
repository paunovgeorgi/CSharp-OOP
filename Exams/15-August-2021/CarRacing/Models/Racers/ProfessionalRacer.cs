using System;
using System.Collections.Generic;
using System.Text;
using CarRacing.Models.Cars.Contracts;

namespace CarRacing.Models.Racers
{
    public class ProfessionalRacer : Racer
    {
        private const int ProfessionalDrivingExperience = 30;
        private const string ProfessionalRacingBehavior = "strict";
        public ProfessionalRacer(string username, ICar car) : base(username, ProfessionalRacingBehavior, ProfessionalDrivingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            DrivingExperience += 10;
        }
    }
}
