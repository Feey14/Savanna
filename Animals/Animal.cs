using Savanna.Game;
using System;
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
            game.Field[WidthCoordinate, HeightCoordinate] = ' ';
        }
        public void Stray(GameEngine game)
        {
            Random random = new Random();
            int x, y;
            do
            {
                x = WidthCoordinate + random.Next(2 + 1) - 1;
                y = HeightCoordinate + random.Next(2 + 1) - 1;
            } while (x < 0 || y < 0 || x >= game.Width || y >= game.Height || game.Field[x, y] != ' ');
            WidthCoordinate = x;
            HeightCoordinate = y;
        }
    }
}