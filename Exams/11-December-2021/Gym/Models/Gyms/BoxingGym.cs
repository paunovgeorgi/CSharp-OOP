﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Gyms
{
    public class BoxingGym : Gym
    {
        private const int BoxingCapacity = 15;
        public BoxingGym(string name) : base(name, BoxingCapacity)
        {
        }
    }
}
