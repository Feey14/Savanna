using Savanna.Game;
using System;
using System.Linq;

namespace Savanna.Animals
{
    public abstract class Animal
    {
        public int WidthCoordinate { get; set; }
        public int HeightCoordinate { get; set; }
        public abstract double Health { get; set; }
        public abstract char AnimalSymbol { get; }

        public abstract void Move(GameEngine game);

        public void Die(GameEngine game)
        {
            game.GameAnimals.Remove(this);
        }

        public void Stray(GameEngine game)
        {
            Random random = new Random();
            int x, y;
            do
            {
                x = WidthCoordinate + random.Next(2 + 1) - 1;
                y = HeightCoordinate + random.Next(2 + 1) - 1;
            } while (x < 0 || y < 0 || x >= game.Width || y >= game.Height || game.GameAnimals.Any(animal => animal.WidthCoordinate == x && animal.HeightCoordinate == y));
            WidthCoordinate = x;
            HeightCoordinate = y;
        }
    }
}