using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavalVessels.Models.Contracts;

namespace NavalVessels.Models
{
    public class Submarine : Vessel, ISubmarine
    {
        private const double InitialArmorThickness = 200;
        public Submarine(string name, double mainWeaponCaliber, double speed) : base(name, mainWeaponCaliber, speed, InitialArmorThickness)
        {
        }

        public override void RepairVessel()
        {
            if (ArmorThickness < InitialArmorThickness)
            {
                ArmorThickness = InitialArmorThickness;
            }
        }

        public bool SubmergeMode { get; private set; }
        public void ToggleSubmergeMode()
        {
            if (!SubmergeMode)
            {
                MainWeaponCaliber += 40;
                Speed -= 4;
                SubmergeMode = true;
            }
            else
            {
                MainWeaponCaliber -= 40;
                Speed += 4;
                SubmergeMode = false;
            }
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(base.ToString());
            if (SubmergeMode)
            {
                sb.AppendLine($" *Submerge mode: ON");
            }
            else
            {
                sb.AppendLine($" *Submerge mode: OFF");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
