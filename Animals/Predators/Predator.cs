using System;
using System.Collections.Generic;

namespace Savanna.Animals.Predators
{
    public abstract class Predator : Animal
    {
        public override int VisionRange => 6;

        /// <summary>
        /// Defines logic for Predator movement priorites are eat animal > chase prey > breed > stray
        /// also removes 0.5 health each move
        /// </summary>
        public override void Move(List<Animal> nearbyanimals, List<BabyAnimal> babyanimals)
        {
            if (EatClosestNonPredator(nearbyanimals) == false)
                if (ChaseClosestNonPredator(nearbyanimals) == false)
                {
                    if (Breed(nearbyanimals, babyanimals) == false)
                    {
                        Stray(nearbyanimals);
                    }
                }
            Health -= 0.5;
        }

        /// <summary>
        /// If its possible eats nearby animal that is located in 1 coordinate interval
        /// If multiple animals can be eatern random animal is eaten
        /// Marks prey as dead, prey coordinates are assigned to predator, health restored to 20
        /// returns true if prey is eaten returns false if theres nothing to eat
        /// </summary>
        private bool EatClosestNonPredator(List<Animal> nearbyanimas)
        {
            var CanEat = nearbyanimas.FindAll(animal => animal.WidthCoordinate <= WidthCoordinate + 1 && animal.WidthCoordinate >= WidthCoordinate - 1 && animal.HeightCoordinate <= HeightCoordinate + 1 && animal.HeightCoordinate >= HeightCoordinate - 1 && animal != this && animal is NonPredators.NonPredator);
            if (CanEat.Count > 0)
            {
                Random randon = new Random();
                Animal target = CanEat[randon.Next(CanEat.Count)];
                if (target.Health > 0)
                {
                    target.Die();
                    WidthCoordinate = target.WidthCoordinate;
                    HeightCoordinate = target.HeightCoordinate;
                    Health = 20;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Looks for closest non predator prey and chases it by moving towards it
        /// Scans area around predator and chases nearest prey
        /// if there are multiple victims chosese random one
        /// </summary>
        private bool ChaseClosestNonPredator(List<Animal> nearbyanimals)
        {
            var lookingforpreyvisionrange = 2;
            var targets = nearbyanimals.FindAll(animal => animal is NonPredators.NonPredator);
            if (targets.Count > 0)
            {
                List<Animal> CanChase = LookingForClosestAnimal(lookingforpreyvisionrange, targets);
                if (CanChase.Count > 0)
                {
                    Random randon = new Random();
                    Animal prey = CanChase[randon.Next(CanChase.Count)];
                    MoveTowardsAnimal(prey, nearbyanimals);
                    return true;
                }
            }
            return false;
        }
    }
}