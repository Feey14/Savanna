using System;

namespace Savanna.Animals
{
    internal class Lion : Animal
    {
        public override char AnimalSymbol { get { return 'L'; } }
        public override double Health { get; set; } = 20;

        public override void Move(Game.GameEngine game)
        {
            if (EatNearbyAntelope(game)) { }
            else if (ChaseNearbyAntelope(game)) { }
            else Stray(game);
            Health -= 0.5;
        }

        public bool EatNearbyAntelope(Game.GameEngine game)
        {
            int visionrange = 1;
            for (int widthvisionrange = -visionrange; widthvisionrange <= visionrange; widthvisionrange++)
                for (int heightvisionrange = -visionrange; heightvisionrange <= visionrange; heightvisionrange++)
                    if (WidthCoordinate + widthvisionrange < 0 || WidthCoordinate + widthvisionrange >= game.Width || HeightCoordinate + heightvisionrange < 0 || HeightCoordinate + heightvisionrange >= game.Height) break;
                    else
                    if (game.Field[WidthCoordinate + widthvisionrange, HeightCoordinate + heightvisionrange] == 'A')
                    {
                        var result = game.GameAnimals.Find(
                            delegate (Animal an)
                            {
                                return an.WidthCoordinate == WidthCoordinate + widthvisionrange && an.HeightCoordinate == HeightCoordinate + heightvisionrange;
                            }
                            );
                        result.Health = 0;
                        WidthCoordinate += widthvisionrange;
                        HeightCoordinate += heightvisionrange;
                        Health = 20;
                        game.Field[WidthCoordinate, HeightCoordinate] = 'L';
                        return true;
                    }
            return false;
        }

        public bool ChaseNearbyAntelope(Game.GameEngine game)
        {
            int visionrange = 5;
            for (int widthvisionrange = -visionrange; widthvisionrange <= visionrange; widthvisionrange++)
                for (int heightvisionrange = -visionrange; heightvisionrange <= visionrange; heightvisionrange++)
                {
                    if (WidthCoordinate + widthvisionrange < 0 || WidthCoordinate + widthvisionrange >= game.Width || HeightCoordinate + heightvisionrange < 0 || HeightCoordinate + heightvisionrange >= game.Height) continue;
                    else
                        if (game.Field[WidthCoordinate + widthvisionrange, HeightCoordinate + heightvisionrange] == 'A')
                    {
                        if (WidthCoordinate + widthvisionrange > WidthCoordinate) WidthCoordinate += 1;
                        else if (WidthCoordinate + widthvisionrange < WidthCoordinate) WidthCoordinate -= 1;
                        if (HeightCoordinate + heightvisionrange > HeightCoordinate) HeightCoordinate += 1;
                        else if (HeightCoordinate + heightvisionrange < HeightCoordinate) HeightCoordinate -= 1;
                        return true;
                    }
                }
            return false;
        }
    }
}