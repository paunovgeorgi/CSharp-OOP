using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach (IAstronaut astronaut in astronauts)
            {
                while (astronaut.Oxygen > 0 && planet.Items.Any())
                {
                    string currentItem = string.Empty;
                    foreach (string item in planet.Items)
                    {
                        astronaut.Bag.Items.Add(item);
                        astronaut.Breath();
                        currentItem = item;
                        break;
                    }
                    planet.Items.Remove(currentItem);
                }

            }
        }
    }
}
