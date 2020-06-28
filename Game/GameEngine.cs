using System;
using System.Collections.Generic;
using System.Text;

namespace Savanna.Game
{
    public class GameEngine
    {
        public int Width { get; set; } = 19;
        public int Height { get; set; } = 19;
        public char[,] Field { get; set; }
        public List<Animals.Animal> GameAnimals { get; set; } = new List<Animals.Animal>();

        public GameEngine()
        {
            Field = new char[Width, Height];
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Field[x, y] = ' ';
        }

        public void PrintField()
        {
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
            } while (Field[randomwidth, randomheight] != ' ');
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
            Field[GameAnimals[i].WidthCoordinate, GameAnimals[i].HeightCoordinate] = ' ';
            GameAnimals[i].Move(this);
            if (GameAnimals[i].Health < 0)
            {
                GameAnimals[i].Die(this);
                return;
            }
            else
            {
                Field[GameAnimals[i].WidthCoordinate, GameAnimals[i].HeightCoordinate] = GameAnimals[i].AnimalSymbol;
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