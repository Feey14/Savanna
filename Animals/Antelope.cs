using System;
using System.Collections.Generic;

namespace Savanna.Animals
{
    internal class Antelope : Animal
    {
        public override char AnimalSymbol { get { return 'A'; } }

        public override double Health { get; set; } = 50;

        public override void Move(Game.GameEngine game)
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