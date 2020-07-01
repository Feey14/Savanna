using System;
using System.Collections.Generic;

namespace Savanna.Animals.NonPredators
{
    public abstract class NonPredator : Animal
    {
        public int VisionRange { get; set; } = 6;

        public override void Move(Game.GameEngine game)
        {
            List<Animal> nearbyanimals = game.GameAnimals.FindAll(animal => animal.WidthCoordinate <= WidthCoordinate + VisionRange && animal.WidthCoordinate >= WidthCoordinate - VisionRange && animal.HeightCoordinate <= HeightCoordinate + VisionRange && animal.HeightCoordinate >= HeightCoordinate - VisionRange && animal != this);
            if (RetreatFromPredator(nearbyanimals) == false)
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

        private bool RetreatFromPredator(List<Animal> nearbyanimals)
        {
            int visionrange = 6;
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
                while (lookingforpredatorvisionrange <= visionrange && RunFrom.Count > 0);
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