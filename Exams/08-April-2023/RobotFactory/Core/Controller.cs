using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using RobotService.Core.Contracts;
using RobotService.Models;
using RobotService.Models.Contracts;
using RobotService.Repositories;
using RobotService.Repositories.Contracts;
using RobotService.Utilities.Messages;

namespace RobotService.Core
{
    public class Controller : IController
    {
        private IRepository<ISupplement> supplements = new SupplementRepository();
        private IRepository<IRobot> robots = new RobotRepository();

        public string CreateRobot(string model, string typeName)
        {
            if (typeName != nameof(DomesticAssistant) && typeName != nameof(IndustrialAssistant))
            {
               // return $"Robot type {typeName} cannot be created.";
                return String.Format(OutputMessages.RobotCannotBeCreated, typeName);
            }

            if (typeName == nameof(DomesticAssistant))
            {
                //IRobot robot = new DomesticAssistant(model);
                robots.AddNew(new DomesticAssistant(model));
            }
            else if (typeName == nameof(IndustrialAssistant))
            {
                robots.AddNew(new IndustrialAssistant(model));
            }

           // return $"{typeName} {model} is created and added to the RobotRepository.";
           return String.Format(OutputMessages.RobotCreatedSuccessfully, typeName, model);
        }

        public string CreateSupplement(string typeName)
        {
            if (typeName != nameof(SpecializedArm) && typeName != nameof(LaserRadar))
            {
               // return $"{typeName} is not compatible with our robots.";
               return String.Format(OutputMessages.SupplementCannotBeCreated, typeName);
            }

            if (typeName == nameof(SpecializedArm))
            {
               supplements.AddNew(new SpecializedArm());
            }
            else if (typeName == nameof(LaserRadar))
            {
                supplements.AddNew(new LaserRadar());
            }

           // return $"{typeName} is created and added to the SupplementRepository.";
           return String.Format(OutputMessages.SupplementCreatedSuccessfully, typeName);
        }

        public string UpgradeRobot(string model, string supplementTypeName)
        {
            ISupplement currentSupplement =
                supplements.Models().FirstOrDefault(s => s.GetType().Name == supplementTypeName);
            int interfaceValue = currentSupplement.InterfaceStandard;

            IEnumerable<IRobot> currentRobots =
                robots.Models().Where(s => !s.InterfaceStandards.Contains(interfaceValue));

            IEnumerable<IRobot> supportedModels = currentRobots.Where(r => r.Model == model);

            if (!supportedModels.Any())
            {
               // return $"All {model} are already upgraded!";
               return String.Format(OutputMessages.AllModelsUpgraded, model);
            }

            supportedModels.FirstOrDefault().InstallSupplement(currentSupplement);
            supplements.RemoveByName(supplementTypeName);

           // return $"{model} is upgraded with {supplementTypeName}.";
           return String.Format(OutputMessages.UpgradeSuccessful, model, supplementTypeName);

        }

        public string RobotRecovery(string model, int minutes)
        {
            IEnumerable<IRobot> robotsToFeed = robots.Models()
                .Where(r => r.Model == model && r.BatteryLevel < r.BatteryCapacity / 2);

            int robotsFed = 0;

            foreach (IRobot robot in robotsToFeed)
            {
                robot.Eating(minutes);
                robotsFed++;
            }

           // return $"Robots fed: {robotsFed}";
           return String.Format(OutputMessages.RobotsFed, robotsFed);
        }

        public string PerformService(string serviceName, int interfaceStandard, int totalPowerNeeded)
        {
            IEnumerable<IRobot> initialRobots =
                robots.Models().Where(r => r.InterfaceStandards.Contains(interfaceStandard)).OrderByDescending(r=>r.BatteryLevel);

            if (!initialRobots.Any())
            {
                //return $"Unable to perform service, {intefaceStandard} not supported!";
                return String.Format(OutputMessages.UnableToPerform, interfaceStandard);
            }

            int batterySum = initialRobots.Sum(r => r.BatteryLevel);

            if (batterySum < totalPowerNeeded)
            {
                //return $"{serviceName} cannot be executed! {totalPowerNeeded - batterySum} more power needed.";
                return String.Format(OutputMessages.MorePowerNeeded, serviceName,
                    (totalPowerNeeded - batterySum));
            }

            int counter = 0;

            foreach (IRobot robot in initialRobots)
            {
                if (robot.BatteryLevel >= totalPowerNeeded)
                {
                    robot.ExecuteService(totalPowerNeeded);
                 counter++;
                 break;
                }
                totalPowerNeeded -= robot.BatteryLevel;
                    robot.ExecuteService(robot.BatteryLevel);
                    counter++;
            }

            //return $"{serviceName} is performed successfully with {counter} robots.";
            return String.Format(OutputMessages.PerformedSuccessfully, serviceName, counter);

        }

        public string Report()
        {
            StringBuilder sb = new();
            foreach (IRobot robot in robots.Models().OrderByDescending(r=>r.BatteryLevel).ThenBy(r=>r.BatteryCapacity))
            {
                sb.AppendLine(robot.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
