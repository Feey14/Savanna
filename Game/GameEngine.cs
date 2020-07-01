﻿using Savanna.Animals;
using Savanna.Animals.Predators;
using System;
using System.Collections.Generic;
using System.Text;

namespace Savanna.Game
{
    public class GameEngine
    {
        public static int Width { get; set; } = 19;
        public static int Height { get; set; } = 19;
        public List<Animal> GameAnimals { get; set; } = new List<Animal>();
        public List<BabyAnimal> UnbornAnimals { get; set; } = new List<BabyAnimal>();

        public void PrintField()
        {
            char[,] Field = new char[Width, Height];
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Field[x, y] = ' ';

            foreach (Animal animal in GameAnimals)
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
            //problem
            // do
            // {
            randomheight = random.Next(Height);
            randomwidth = random.Next(Width);
            // } while (!GameAnimals.Exists(an => an.WidthCoordinate == randomwidth && an.HeightCoordinate == randomheight));
            animal.WidthCoordinate = randomwidth;
            animal.HeightCoordinate = randomheight;
            GameAnimals.Add(animal);
        }

        /// <summary>
        /// To make it more conviniet and meke it easier to follow the game. Lions make moves first its acheived by sorting animal list
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
            for (int i = UnbornAnimals.Count - 1; i >= 0; i--)
            {
                if (UnbornAnimals[i].Move(this) == false)
                    UnbornAnimals[i].Die(this);
            }
        }

        private void IteratingProcess(int i)
        {
            GameAnimals[i].Move(this);
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