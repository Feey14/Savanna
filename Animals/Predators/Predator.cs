using System;
using System.Collections.Generic;

namespace Savanna.Animals.Predators
{
    public abstract class Predator : Animal
    {
        public int VisionRange { get; set; } = 6;

        public override void Move(Game.GameEngine game)
        {
            List<Animal> nearbyanimals = game.GameAnimals.FindAll(animal => animal.WidthCoordinate <= WidthCoordinate + VisionRange && animal.WidthCoordinate >= WidthCoordinate - VisionRange && animal.HeightCoordinate <= HeightCoordinate + VisionRange && animal.HeightCoordinate >= HeightCoordinate - VisionRange && animal != this);
            if (EatClosestNonPredator(nearbyanimals) == false)
                if (ChaseClosestNonPredator(nearbyanimals) == false)
                {
                    BabyAnimal child = game.UnbornAnimals.Find(unbornanimal => unbornanimal.Parent1 == this || unbornanimal.Parent2 == this);
                    if (child != null)
                    {
                        if (child.RoundCount == 3)
                        {
                            child.Move(game);
                            this.Stray(nearbyanimals);
                        }
                    }
                    else
                    {
                        if (Breed(game.UnbornAnimals, nearbyanimals) != null)
                        {
                            game.UnbornAnimals.Add(Breed(game.UnbornAnimals, nearbyanimals));
                        }
                        else Stray(nearbyanimals);
                    }
                }
            Health -= 0.5;
        }

        private bool EatClosestNonPredator(List<Animal> nearbyanimas)
        {
            var CanEat = nearbyanimas.FindAll(animal => animal.WidthCoordinate <= WidthCoordinate + 1 && animal.WidthCoordinate >= WidthCoordinate - 1 && animal.HeightCoordinate <= HeightCoordinate + 1 && animal.HeightCoordinate >= HeightCoordinate - 1 && animal != this);
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
            var visionrange = 6;
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
                while (lookingforpreyvisionrange <= visionrange && CanChase.Count > 0);
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