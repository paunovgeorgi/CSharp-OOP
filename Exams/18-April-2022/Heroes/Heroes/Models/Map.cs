using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;

namespace Heroes.Models
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> knights = players.Where(p => p.GetType().Name == nameof(Knight) && p.Weapon != null).ToList();
            List<IHero> barbarians = players.Where(p => p.GetType().Name == nameof(Barbarian) && p.Weapon != null).ToList();
            int aliveKnights = knights.Where(k => k.IsAlive).Count();
            int aliveBarbarians = barbarians.Where(b => b.IsAlive).Count();

            while (knights.Any(k=>k.Health > 0) && barbarians.Any(b=>b.Health > 0))
            {
                foreach (IHero knight in knights.Where(k=>k.Health > 0))
                {
                    foreach (IHero barbarian in barbarians.Where(b=>b.Health > 0))
                        {
                            barbarian.TakeDamage(knight.Weapon.DoDamage());
                        }
                }
                foreach (IHero barbarian in barbarians.Where(b=>b.Health > 0))
                {
                    foreach (IHero k in knights.Where(k=>k.Health> 0))
                        {
                            k.TakeDamage(barbarian.Weapon.DoDamage());
                        }
                }
            }

            if (knights.Any(k=>k.Health > 0))
            {
                int casulties = aliveKnights - knights.Where(k => k.Health > 0).Count();
                return String.Format(OutputMessages.MapFightKnightsWin, casulties);
            }
            int barCasulties = aliveBarbarians - barbarians.Where(b => b.Health > 0).Count();
                return String.Format(OutputMessages.MapFigthBarbariansWin, barCasulties);
            
        }
    }
}
