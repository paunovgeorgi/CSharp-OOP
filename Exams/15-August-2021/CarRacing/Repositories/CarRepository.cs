﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Repositories.Contracts;
using CarRacing.Utilities.Messages;

namespace CarRacing.Repositories
{
    public class CarRepository : IRepository<ICar>
    {
        private readonly List<ICar> cars = new List<ICar>();
        public IReadOnlyCollection<ICar> Models => cars;
        public void Add(ICar model)
        {
            if (model == null)
            {
                throw new ArgumentException(ExceptionMessages.InvalidAddCarRepository);
            }
            cars.Add(model);
        }

        public bool Remove(ICar model)
        {
            return cars.Remove(model);
        }

        public ICar FindBy(string property)
        {
            return cars.FirstOrDefault(c => c.VIN == property);
        }
    }
}
