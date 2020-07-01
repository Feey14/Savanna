using System;
using System.Collections.Generic;

namespace Savanna.Animals.Predators
{
    public abstract class Predator : Animal
    {
        public override int VisionRange { get; set; } = 6;

        public override void Move(List<Animal> nearbyanimals, List<BabyAnimal> babyanimals)
        {
            if (EatClosestNonPredator(nearbyanimals) == false)
                if (ChaseClosestNonPredator(nearbyanimals) == false)
                {
                    if (BreedingProcess(babyanimals, nearbyanimals) == false)
                    {
                        Stray(nearbyanimals);
                    }
                }
            Health -= 0.5;
        }

        private bool EatClosestNonPredator(List<Animal> nearbyanimas)
        {
            var CanEat = nearbyanimas.FindAll(animal => animal.WidthCoordinate <= WidthCoordinate + 1 && animal.WidthCoordinate >= WidthCoordinate - 1 && animal.HeightCoordinate <= HeightCoordinate + 1 && animal.HeightCoordinate >= HeightCoordinate - 1 && animal != this && animal is NonPredators.NonPredator);
            if (CanEat.Count > 0)
            {
                Random randon = new Random();
                Animal target = CanEat[randon.Next(CanEat.Count)];
                if (target.Health > 0)
                {
                    target.Health = 0;
                    WidthCoordinate = target.WidthCoordinate;
                    HeightCoordinate = target.HeightCoordinate;
                    Health = 20;
                }
                return true;
            }
            return false;
        }

        private bool ChaseClosestNonPredator(List<Animal> nearbyanimals)
        {
            var lookingforpreyvisionrange = 2;
            List<Animal> CanChase;

            var targets = nearbyanimals.FindAll(animal => animal is NonPredators.NonPredator);
            if (targets.Count > 0)
            {
                do
                {
                    //Looking for closeset pery
                    CanChase = nearbyanimals.FindAll(animal => animal.WidthCoordinate <= WidthCoordinate + lookingforpreyvisionrange && animal.WidthCoordinate >= WidthCoordinate - lookingforpreyvisionrange && animal.HeightCoordinate <= HeightCoordinate + lookingforpreyvisionrange && animal.HeightCoordinate >= HeightCoordinate - lookingforpreyvisionrange && animal != this);
                    lookingforpreyvisionrange++;
                }
                while (lookingforpreyvisionrange <= VisionRange && CanChase.Count > 0);
                if (CanChase.Count > 0)
                {
                    Random randon = new Random();
                    Animal target = CanChase[randon.Next(CanChase.Count)];
                    MoveTowardsAnimal(target, nearbyanimals);
                    return true;
                }
            }
            return false;
        }
    }
}