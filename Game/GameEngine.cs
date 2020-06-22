using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Savanna.Game
{
    public class GameEngine
    {
        public int Width { get; set; } = 19;
        public int Height { get; set; } = 19;
        public List<Animals.Animal> GameAnimals { get; set; } = new List<Animals.Animal>();
        public void PrintField()
        {
            char [,] Field = new char[Width, Height];
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Field[x, y] = ' ';
            foreach (var animal in GameAnimals)
            {
                Field[animal.WidthCoordinate, animal.HeightCoordinate] = animal.AnimalSymbol;
            }

            StringBuilder line = new StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                line.Clear();
                for (int x = 0; x < Width; x++)
                {
                    line.Append(Field[x, y]);
                }
                Console.WriteLine(line.Append("|"));
            }
            line.Clear();
            for (int x = 0; x <= Width; x++)
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
                randomheight = random.Next(Height);
                randomwidth = random.Next(Width);
            } while (GameAnimals.Any(animal => animal.WidthCoordinate == randomwidth && animal.HeightCoordinate == randomheight));
            animal.WidthCoordinate = randomwidth;
            animal.HeightCoordinate = randomheight;
            GameAnimals.Add(animal);
        }

        /// <summary>
        /// To make it more conviniet and meke it easier to follow the game. Lions make moves first
        /// </summary>
        public void Iterate()
        {
            SortAnimals();
            for (int i = GameAnimals.Count - 1; i >= 0; i--)
            {
                if (GameAnimals[i] is Animals.Lion)
                {
                    IteratingProcess(i);
                }
            }
            for (int i = GameAnimals.Count - 1; i >= 0; i--)
            {
                if (GameAnimals[i] is Animals.Antelope)
                {
                    IteratingProcess(i);
                }
            }
        }

        public void IteratingProcess(int i)
        {
            GameAnimals[i].Move(this);
            {
                if (GameAnimals[i].Health < 0)
                {
                    GameAnimals[i].Die(this);
                    return;
                }
            }
        }

        /// <summary>
        /// Sorting Animal List by type
        /// </summary>
        public void SortAnimals()
        {
            GameAnimals.Sort((a, b) => a.GetType().FullName.CompareTo(b.GetType().FullName));
        }
    }
}