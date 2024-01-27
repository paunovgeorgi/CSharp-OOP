﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Gym.Models.Equipment
{
    public class Kettlebell : Equipment
    {
        private const double KEttlebellWeight = 10_000;
        private const decimal KettlebellPrice = 80;
        public Kettlebell() : base(KEttlebellWeight, KettlebellPrice)
        {
        }
    }
}
