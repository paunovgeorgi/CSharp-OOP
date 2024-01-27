using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NavalVessels.Core.Contracts;
using NavalVessels.Models;
using NavalVessels.Models.Contracts;
using NavalVessels.Repositories;
using NavalVessels.Repositories.Contracts;
using NavalVessels.Utilities.Messages;

namespace NavalVessels.Core
{
    public class Controller : IController
    {
        private IRepository<IVessel> vessels = new VesselRepository();
        private readonly List<ICaptain> captains = new List<ICaptain>();
        public string HireCaptain(string fullName)
        {
            if (captains.Any(c=>c.FullName == fullName))
            {
                return String.Format(OutputMessages.CaptainIsAlreadyHired, fullName);
            }
            captains.Add(new Captain(fullName));
            return String.Format(OutputMessages.SuccessfullyAddedCaptain, fullName);
        }

        public string ProduceVessel(string name, string vesselType, double mainWeaponCaliber, double speed)
        {
            if (vessels.Models.Any(v=>v.Name == name))
            {
                return String.Format(OutputMessages.VesselIsAlreadyManufactured, vessels.FindByName(name).GetType().Name, name);
            }

            if (vesselType != nameof(Submarine) && vesselType != nameof(Battleship))
            {
                return String.Format(OutputMessages.InvalidVesselType);
            }

            IVessel vessel;
            if (vesselType == nameof(Submarine))
            {
                vessel = new Submarine(name, mainWeaponCaliber, speed);
            }
            else
            {
                vessel = new Battleship(name, mainWeaponCaliber, speed);
            }

            vessels.Add(vessel);
            return String.Format(OutputMessages.SuccessfullyCreateVessel, vesselType, name, mainWeaponCaliber, speed);
        }

        public string AssignCaptain(string selectedCaptainName, string selectedVesselName)
        {
            if (!captains.Any(c => c.FullName == selectedCaptainName))
            {
                return String.Format(OutputMessages.CaptainNotFound, selectedCaptainName);
            }

            if (vessels.FindByName(selectedVesselName) == null)
            {
                return String.Format(OutputMessages.VesselNotFound, selectedVesselName);
            }

            IVessel vessel = vessels.FindByName(selectedVesselName);
            if (vessel.Captain != null)
            {
                return String.Format(OutputMessages.VesselOccupied, selectedVesselName);
            }

            ICaptain captain = captains.FirstOrDefault(c => c.FullName == selectedCaptainName);
            vessel.Captain = captain;
            captain.AddVessel(vessel);

            return String.Format(OutputMessages.SuccessfullyAssignCaptain, selectedCaptainName, selectedVesselName);
        }

        public string CaptainReport(string captainFullName)
        {
            ICaptain captain = captains.FirstOrDefault(c => c.FullName == captainFullName && c.Vessels.Any());
            return captain.Report();
        }

        public string VesselReport(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);
            return vessel.ToString();
        }

        public string ToggleSpecialMode(string vesselName)
        {
            IVessel vessel = vessels.FindByName(vesselName);

            if (vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, vesselName);
            }

            if (vessel.GetType().Name == nameof(Battleship))
            {
                IBattleship battleship = vessel as Battleship;
                battleship.ToggleSonarMode();
                return String.Format(OutputMessages.ToggleBattleshipSonarMode, vesselName);
            }

            ISubmarine submarine = vessel as Submarine;
            submarine.ToggleSubmergeMode();
            return String.Format(OutputMessages.ToggleSubmarineSubmergeMode, vesselName);
        }

        public string AttackVessels(string attackingVesselName, string defendingVesselName)
        {
            IVessel attacking = vessels.FindByName(attackingVesselName);
            IVessel defending = vessels.FindByName(defendingVesselName);
            if (attacking == null)
            {
                return String.Format(OutputMessages.VesselNotFound, attackingVesselName);
            }

            if (defending == null)
            {
                return String.Format(OutputMessages.VesselNotFound, defendingVesselName);
            }

            if (attacking.ArmorThickness == 0)
            {
                return String.Format(OutputMessages.AttackVesselArmorThicknessZero, attackingVesselName);
            }

            if (defending.ArmorThickness == 0)
            {
                return String.Format(OutputMessages.AttackVesselArmorThicknessZero, defendingVesselName);
            }

            attacking.Attack(defending);

            attacking.Captain.IncreaseCombatExperience();
            defending.Captain.IncreaseCombatExperience();

            return String.Format(OutputMessages.SuccessfullyAttackVessel, defendingVesselName, attackingVesselName,
                defending.ArmorThickness);
        }

        public string ServiceVessel(string vesselName)
        {

            IVessel vessel = vessels.FindByName(vesselName);
            if (vessel == null)
            {
                return String.Format(OutputMessages.VesselNotFound, vesselName);
            }

            vessel.RepairVessel();

            return String.Format(OutputMessages.SuccessfullyRepairVessel, vesselName);
        }
    }
}
