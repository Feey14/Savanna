using System;
using System.Collections.Generic;
using System.Text;

namespace Savanna.Game
{
    public class GameEngine
    {
        public int Width { get; set; } = 19;
        public int Height { get; set; } = 19;
        public char[,] Matrix { get; set; }
        public List<Animals.Animal> GameAnimals { get; set; } = new List<Animals.Animal>();

        public GameEngine() // Constructor class that creates Matrix
        {
            Matrix = new char[Width, Height];
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Matrix[x, y] = ' ';
        }

        public void PrintField()//Printing Game Field
        {
            StringBuilder line = new StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                line.Clear();
                for (int x = 0; x < Width; x++)
                {
                    line.Append(Matrix[x, y]);
                }
                Console.WriteLine(line.Append("|"));// Printing line and right border symbol
            }
            line.Clear();
            for (int x = 0; x <= Width; x++)// Getting bottom border string
            {
                line.Append("-");
            }
            Console.WriteLine(line.ToString());//Printing bottom border
        }

        public void AddAnimal(Animals.Animal animal)
        {
            Random random = new Random();
            int randomheight, randomwidth;
            do
            {
                randomheight = random.Next(Height);
                randomwidth = random.Next(Width);
            } while (Matrix[randomwidth, randomheight] != ' ');

            switch (animal)// Should be changed mby put it in animal function
            {
                case Animals.Lion lion:
                    Matrix[randomwidth, randomheight] = lion.AnimalSymbol;
                    lion.WidthCoordinate = randomwidth;
                    lion.HeightCoordinate = randomheight;
                    GameAnimals.Add(lion);
                    break;

                case Animals.Antelope antelope:
                    Matrix[randomwidth, randomheight] = antelope.AnimalSymbol;
                    antelope.WidthCoordinate = randomwidth;
                    antelope.HeightCoordinate = randomheight;
                    GameAnimals.Add(antelope);
                    break;
            }
        }

        public void Iterate()
        {
            List<Animals.Animal> newlist = new List<Animals.Animal>(GameAnimals);
            foreach (Animals.Animal animal in GameAnimals)
            {
                Matrix[animal.WidthCoordinate, animal.HeightCoordinate] = ' ';
                animal.Move(this, newlist);
                Matrix[animal.WidthCoordinate, animal.HeightCoordinate] = animal.AnimalSymbol;
            }
            GameAnimals = newlist;
        }
    }
}