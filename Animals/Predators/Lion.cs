﻿using AnimalTypeClassLibrary;

namespace Savanna.Animals.Predators
{
    /// <summary>
    /// Creating Lion that inherits from Predator class
    /// setting Lion health to 20 and vision range to 6
    /// </summary>
    public class Lion : Predator
    {
        public override char AnimalSymbol { get { return 'L'; } }
        public override double Health { get; set; } = 20;
        public override int VisionRange => 6;
    }
}