using Savanna.Game;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Savanna.Animals
{
    public abstract class Animal
    {
        public int WidthCoordinate { get; set; }
        public int HeightCoordinate { get; set; }
        public abstract double Health { get; set; }
        public abstract int VisionRange { get; set; }
        public abstract char AnimalSymbol { get; }

        public abstract void Move(List<Animal> nearbyanimals, List<BabyAnimal> unbornanimals);

        /// <summary>
        /// Deletes Animal from the game
        /// </summary>
        public void Die()
        {
            Health = 0;
        }

        /// <summary>
        /// Function that makes animal stray in random direction if field is empty
        /// </summary>
        public void Stray(List<Animal> nearbyanimals)
        {
            Random random = new Random();
            int x, y;
            do
            {
                x = WidthCoordinate + random.Next(2 + 1) - 1;
                y = HeightCoordinate + random.Next(2 + 1) - 1;
            } while (x < 0 || y < 0 || x >= GameEngine.Width || y >= GameEngine.Height || !IsEmpty(x, y, nearbyanimals));
            WidthCoordinate = x;
            HeightCoordinate = y;
        }

        /// <summary>
        /// Breed function makes BabyAnimal and if parent are in the same position for 3 rounds animal is born
        /// </summary>
        public BabyAnimal Breed(List<Animal> nearbyanimals)
        {
            if (WidthCoordinate > 0 && (nearbyanimals.Any(an => an.WidthCoordinate == WidthCoordinate - 1 && an.HeightCoordinate == HeightCoordinate && an.GetType().Equals(this.GetType()))))
            {
                Animal result = nearbyanimals.Find(an => an.WidthCoordinate == WidthCoordinate - 1 && an.HeightCoordinate == HeightCoordinate);
                BabyAnimal child = new BabyAnimal(this, result);
                return child;
            }
            return null;
        }

        public bool BreedingProcess(List<BabyAnimal> babyanimals, List<Animal> nearbyanimals)
        {
            BabyAnimal child = babyanimals.Find(unbornanimal => unbornanimal.Parent1 == this || unbornanimal.Parent2 == this);
            if (child != null)
            {
                if (child.RoundCount > 3)
                {
                    this.Stray(nearbyanimals);
                }
                return true;
            }
            else
            {
                var result = Breed(nearbyanimals);
                if (result != null)
                {
                    babyanimals.Add(result);
                    return true;
                }
                else return false;
            }
        }

        public void MoveTowardsAnimal(Animal target, List<Animal> nearbyanimals)
        {
            if (target.WidthCoordinate > WidthCoordinate && IsEmpty(WidthCoordinate + 1, HeightCoordinate, nearbyanimals)) WidthCoordinate += 1;
            else if (target.WidthCoordinate < WidthCoordinate && IsEmpty(WidthCoordinate - 1, HeightCoordinate, nearbyanimals)) WidthCoordinate -= 1;
            if (target.HeightCoordinate > HeightCoordinate && IsEmpty(WidthCoordinate, HeightCoordinate + 1, nearbyanimals)) HeightCoordinate += 1;
            else if (target.HeightCoordinate < HeightCoordinate && IsEmpty(WidthCoordinate, HeightCoordinate - 1, nearbyanimals)) HeightCoordinate -= 1;
        }

        public void RunFromAnimal(Animal target, List<Animal> nearbyanimals)
        {
            if (WidthCoordinate > 0)
            {
                if (target.WidthCoordinate > WidthCoordinate && IsEmpty(WidthCoordinate - 1, HeightCoordinate, nearbyanimals)) WidthCoordinate -= 1;
                else if (target.WidthCoordinate == WidthCoordinate && IsEmpty(WidthCoordinate - 1, HeightCoordinate, nearbyanimals)) WidthCoordinate -= 1;
            }
            else if (WidthCoordinate < Game.GameEngine.Width - 1)
            {
                if (target.WidthCoordinate < WidthCoordinate && IsEmpty(WidthCoordinate + 1, HeightCoordinate, nearbyanimals)) WidthCoordinate += 1;
                else if (target.WidthCoordinate == WidthCoordinate && IsEmpty(WidthCoordinate + 1, HeightCoordinate, nearbyanimals)) WidthCoordinate += 1;
            }

            if (HeightCoordinate > 0)
            {
                if (target.HeightCoordinate > HeightCoordinate && HeightCoordinate > 0 && IsEmpty(WidthCoordinate, HeightCoordinate - 1, nearbyanimals)) HeightCoordinate -= 1;
                else if (target.HeightCoordinate == HeightCoordinate && IsEmpty(WidthCoordinate, HeightCoordinate - 1, nearbyanimals)) HeightCoordinate -= 1;
            }
            else if (HeightCoordinate < Game.GameEngine.Height - 1)
            {
                if (target.HeightCoordinate < HeightCoordinate && IsEmpty(WidthCoordinate, HeightCoordinate + 1, nearbyanimals)) HeightCoordinate += 1;
                else if (target.HeightCoordinate == HeightCoordinate && IsEmpty(WidthCoordinate, HeightCoordinate + 1, nearbyanimals)) HeightCoordinate += 1;
            }
        }

        private bool IsEmpty(int widthcoordinate, int heightcoordinate, List<Animal> nearbyanimals)
        {
            if (nearbyanimals.Any(an => an.WidthCoordinate == widthcoordinate && an.HeightCoordinate == heightcoordinate))
                return false;
            else return true;
        }
    }
}