using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Gym.Models.Equipment.Contracts;
using Gym.Repositories.Contracts;

namespace Gym.Repositories
{
    public class EquipmentRepository : IRepository<IEquipment>
    {
        private readonly List<IEquipment> equipments = new List<IEquipment>();
        public IReadOnlyCollection<IEquipment> Models => equipments;
        public void Add(IEquipment model)
        {
            equipments.Add(model);
        }

        public bool Remove(IEquipment model)
        {
           return equipments.Remove(model);
        }

        public IEquipment FindByType(string type)
        {
            return equipments.FirstOrDefault(e => e.GetType().Name == type);
        }
    }
}
