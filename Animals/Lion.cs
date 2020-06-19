using System;
using System.Collections.Generic;

namespace Savanna.Animals
{
    internal class Lion : Animal
    {
        public override char AnimalSymbol { get { return 'L'; } }

        public override void Move(Game.GameEngine game, List<Animal> newlist)
        {
            if (EatNearbyAntelope(game, newlist)) { }
            else
            {
                Random random = new Random();
                int x, y;
                do
                {
                    x = WidthCoordinate + random.Next(2 + 1) - 1;
                    y = HeightCoordinate + random.Next(2 + 1) - 1;
                } while (x < 0 || y < 0 || x >= game.Width || y >= game.Height || game.Matrix[x, y] != ' ');
                WidthCoordinate = x;
                HeightCoordinate = y;
            }
        }

        public bool EatNearbyAntelope(Game.GameEngine game, List<Animal> newlist)
        {
            for (int widthvisionrange = -1; widthvisionrange <= 1; widthvisionrange++)
                for (int heightvisionrange = -1; heightvisionrange <= 1; heightvisionrange++)
                    if (WidthCoordinate + widthvisionrange < 0 || WidthCoordinate + widthvisionrange >= game.Width || HeightCoordinate + heightvisionrange < 0 || HeightCoordinate + heightvisionrange >= game.Height) break;
                    else
                    if (game.Matrix[WidthCoordinate + widthvisionrange, HeightCoordinate + heightvisionrange] == 'A')
                    {
                        var result = game.GameAnimals.Find(
                            delegate (Animal an)
                            {
                                return an.WidthCoordinate == WidthCoordinate + widthvisionrange && an.HeightCoordinate == HeightCoordinate + heightvisionrange;
                            }
                            );
                        newlist.Remove(result);
                        WidthCoordinate += widthvisionrange;
                        HeightCoordinate += heightvisionrange;
                        game.Matrix[WidthCoordinate, HeightCoordinate] = 'L';
                        return true;
                    }
            return false;
        }
    }
}