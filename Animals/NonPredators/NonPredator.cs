using System;
using System.Collections.Generic;

namespace Savanna.Animals.NonPredators
{
    public abstract class NonPredator : Animal
    {
        public override int VisionRange { get; set; } = 6;

        public override void Move(List<Animal> nearbyanimals, List<BabyAnimal> unbornanimals)
        {
            if (RetreatFromPredator(nearbyanimals) == false)
            {
                if (BreedingProcess(unbornanimals, nearbyanimals) == false)
                {
                    Stray(nearbyanimals);
                }
            }
            Health -= 0.5;
        }

        private bool RetreatFromPredator(List<Animal> nearbyanimals)
        {
            int lookingforpredatorvisionrange = 1;
            List<Animal> RunFrom = new List<Animal>();
            var predators = nearbyanimals.FindAll(animal => animal is Predators.Predator);
            if (predators.Count > 0)
            {
                do
                {
                    //Looking for closeset pery
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