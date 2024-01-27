using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavalVessels.Models.Contracts;

namespace NavalVessels.Models
{
    public class Battleship : Vessel, IBattleship
    {
        private const double InitialArmorThickness = 300;
        public Battleship(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, InitialArmorThickness)
        {
        }

        public override void RepairVessel()
        {
            if (ArmorThickness < InitialArmorThickness)
            {
                ArmorThickness = InitialArmorThickness;
            }
        }

        public bool SonarMode { get; private set; }
        public void ToggleSonarMode()
        {
            if (!SonarMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 5;
                SonarMode = true;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 5;
                SonarMode = false;
            }

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            if (SonarMode)
            {
                sb.AppendLine($" *Sonar mode: ON");
            }
            else
            {
                sb.AppendLine($" *Sonar mode: OFF");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
