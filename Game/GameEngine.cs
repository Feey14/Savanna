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

        public GameEngine() // Constructor class that creates Field
        {
            Field = new char[Width, Height];
            for (int y = 0; y < Height; y++)
                for (int x = 0; x < Width; x++)
                    Field[x, y] = ' ';
        }

        public void PrintField()//Printing Game Field
        {
            StringBuilder line = new StringBuilder();
            for (int y = 0; y < Height; y++)
            {
                line.Clear();
                for (int x = 0; x < Width; x++)
                {
                    line.Append(Field[x, y]);
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
            } while (Field[randomwidth, randomheight] != ' ');
            Field[randomwidth, randomheight] = animal.AnimalSymbol;
            animal.WidthCoordinate = randomwidth;
            animal.HeightCoordinate = randomheight;
            GameAnimals.Add(animal);
        }

        public void Iterate()
        {
            List<Animals.Animal> newlist = new List<Animals.Animal>(GameAnimals);
            foreach (Animals.Animal animal in GameAnimals)
            {
                if (animal.Health > 0)
                {
                    Field[animal.WidthCoordinate, animal.HeightCoordinate] = ' ';
                    animal.Move(this);
                    Field[animal.WidthCoordinate, animal.HeightCoordinate] = animal.AnimalSymbol;
                }
                if(animal.Health <= 0)
                {
                    animal.Die();
                }
            }
            GameAnimals = newlist;
        }
    }
}