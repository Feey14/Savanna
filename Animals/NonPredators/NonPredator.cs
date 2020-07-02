﻿using System;
using System.Collections.Generic;

namespace Savanna.Animals.NonPredators
{
    public abstract class NonPredator : Animal
    {
        public override int VisionRange => 6;

        /// <summary>
        /// Defines logic for NonPredator movement priorites are Retreat from predator > breed > stray
        /// also removes 0.5 health each move
        /// </summary>
        public override void Move(List<Animal> nearbyanimals, List<BabyAnimal> babyanimals)
        {
            if (RetreatFromPredator(nearbyanimals) == false)
            {
                if (Breed(nearbyanimals, babyanimals) == false)
                {
                    Stray(nearbyanimals);
                }
            }
            Health -= 0.5;
        }

        /// <summary>
        /// Looks for closest predator and runs from it
        /// Scans area around animal
        /// if there are multiple predator choseses random one and runs from it
        /// </summary>
        private bool RetreatFromPredator(List<Animal> nearbyanimals)
        {
            int lookingforpredatorvisionrange = 1;
            List<Animal> RunFrom = new List<Animal>();
            var predators = nearbyanimals.FindAll(animal => animal is Predators.Predator);
            if (predators.Count > 0)
            {
                do
                {
                    //Looking for closeset animal
                    RunFrom = nearbyanimals.FindAll(animal => animal.WidthCoordinate <= WidthCoordinate + lookingforpredatorvisionrange && animal.WidthCoordinate >= WidthCoordinate - lookingforpredatorvisionrange && animal.HeightCoordinate <= HeightCoordinate + lookingforpredatorvisionrange && animal.HeightCoordinate >= HeightCoordinate - lookingforpredatorvisionrange && animal != this);
                    lookingforpredatorvisionrange++;
                }
                while (lookingforpredatorvisionrange <= VisionRange && RunFrom.Count > 0);
                if (RunFrom.Count > 0)
                {
                    Random randon = new Random();
                    Animal predator = RunFrom[randon.Next(RunFrom.Count)];
                    RunFromAnimal(predator, nearbyanimals);
                    return true;
                }
            }
            return false;
        }
    }
}