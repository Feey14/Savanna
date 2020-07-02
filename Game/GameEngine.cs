using Savanna.Animals;
using Savanna.Animals.Predators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Savanna.Game
{
    public class GameEngine
    {

        public List<Animal> GameAnimals { get; set; } = new List<Animal>();
        public List<BabyAnimal> BabyAnimals { get; set; } = new List<BabyAnimal>();

        public void PrintField()
        {
            char[,] Field = new char[GameEnvironment.Width, GameEnvironment.Height];
            for (int y = 0; y < GameEnvironment.Height; y++)
                for (int x = 0; x < GameEnvironment.Width; x++)
                    Field[x, y] = ' ';

            foreach (Animal animal in GameAnimals)
            {
                Field[animal.WidthCoordinate, animal.HeightCoordinate] = animal.AnimalSymbol;
            }

            StringBuilder line = new StringBuilder();
            for (int y = 0; y < GameEnvironment.Height; y++)
            {
                line.Clear();
                for (int x = 0; x < GameEnvironment.Width; x++)
                {
                    line.Append(Field[x, y]);
                }
                Console.WriteLine(line.Append("|"));
            }
            line.Clear();
            for (int x = 0; x <= GameEnvironment.Width; x++)
            {
                line.Append("-");
            }
            Console.WriteLine(line.ToString());
        }

        public void AddAnimal(Animals.Animal animal)
        {
            Random random = new Random();
            int randomheight, randomwidth;
            do
            {
            randomheight = random.Next(GameEnvironment.Height);
            randomwidth = random.Next(GameEnvironment.Width);
            } while (GameAnimals.Exists(an => an.WidthCoordinate == randomwidth && an.HeightCoordinate == randomheight));
            animal.WidthCoordinate = randomwidth;
            animal.HeightCoordinate = randomheight;
            GameAnimals.Add(animal);
        }

        /// <summary>
        /// To make it more conviniet and meke it easier to follow the game. Predators make moves first its acheived by sorting animal list
        /// </summary>
        public void Iterate()
        {
            SortAnimals();
            for (int i = GameAnimals.Count - 1; i >= 0; i--)
            {
                if (GameAnimals[i] is Predator)
                    IteratingProcess(i);
            }
            for (int i = GameAnimals.Count - 1; i >= 0; i--)
            {
                if (GameAnimals[i] is Animals.NonPredators.NonPredator)
                    IteratingProcess(i);
            }
            for (int i = BabyAnimals.Count - 1; i >= 0; i--)
            {
                var result = BabyAnimals[i].Move();
                if (result != null)
                    AddAnimal(result);
                else if (BabyAnimals[i].RoundCount > 3 && result == null)
                {
                    //Problem got error here
                    BabyAnimals.RemoveAt(i);
                }
            }
        }

        private void IteratingProcess(int i)
        {
            List<Animal> nearbyanimals = GameAnimals.FindAll(animal => animal.WidthCoordinate <= GameAnimals[i].WidthCoordinate + GameAnimals[i].VisionRange && animal.WidthCoordinate >= GameAnimals[i].WidthCoordinate - GameAnimals[i].VisionRange && animal.HeightCoordinate <= GameAnimals[i].HeightCoordinate + GameAnimals[i].VisionRange && animal.HeightCoordinate >= GameAnimals[i].HeightCoordinate - GameAnimals[i].VisionRange && animal != GameAnimals[i]);
            GameAnimals[i].Move(nearbyanimals, BabyAnimals);
            if (GameAnimals[i].Health < 0)
            {
                GameAnimals.RemoveAt(i);
                return;
            }
        }

        /// <summary>
        /// Sorting Animal List by type
        /// </summary>
        protected void SortAnimals()
        {
            GameAnimals.Sort((a, b) => a.GetType().FullName.CompareTo(b.GetType().FullName));
        }
    }
}