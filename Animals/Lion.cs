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
                        if (game.GameAnimals.Find(animal => animal.WidthCoordinate == WidthCoordinate + widthvisionrange && animal.HeightCoordinate == HeightCoordinate + heightvisionrange) is Antelope)
                    {
                        var result = game.GameAnimals.Find(
                            delegate (Animal an)
                            {
                                return an.WidthCoordinate == WidthCoordinate + widthvisionrange && an.HeightCoordinate == HeightCoordinate + heightvisionrange;
                            }
                            );
                        if (result.Health > 0)
                        {
                            result.Health = 0;
                            WidthCoordinate += widthvisionrange;
                            HeightCoordinate += heightvisionrange;
                            Health = 20;
                        }
                        return true;
                    }
            return false;
        }

        public bool ChaseNearbyAntelope(Game.GameEngine game)
        {
            int visionrange = 6;
            for (int widthvisionrange = -visionrange; widthvisionrange <= visionrange; widthvisionrange++)
                for (int heightvisionrange = -visionrange; heightvisionrange <= visionrange; heightvisionrange++)
                {
                    if (WidthCoordinate + widthvisionrange < 0 || WidthCoordinate + widthvisionrange >= game.Width || HeightCoordinate + heightvisionrange < 0 || HeightCoordinate + heightvisionrange >= game.Height) continue;
                    else
                        if (game.GameAnimals.Find(animal => animal.WidthCoordinate == WidthCoordinate + widthvisionrange && animal.HeightCoordinate == HeightCoordinate + heightvisionrange) is Antelope)
                    {
                        int targetwidthcoord = WidthCoordinate + widthvisionrange;
                        int targetheightcoord = HeightCoordinate + heightvisionrange;
                        if (targetwidthcoord > WidthCoordinate && !game.GameAnimals.Exists(animal => animal.WidthCoordinate == targetwidthcoord + 1 && animal.HeightCoordinate == targetheightcoord)) WidthCoordinate += 1;
                        else if (targetwidthcoord < WidthCoordinate && !game.GameAnimals.Exists(animal => animal.WidthCoordinate == targetwidthcoord - 1 && animal.HeightCoordinate == targetheightcoord)) WidthCoordinate -= 1;
                        if (targetheightcoord > HeightCoordinate && !game.GameAnimals.Exists(animal => animal.WidthCoordinate == targetwidthcoord && animal.HeightCoordinate == targetheightcoord + 1)) HeightCoordinate += 1;
                        else if (targetheightcoord < HeightCoordinate && !game.GameAnimals.Exists(animal => animal.WidthCoordinate == targetwidthcoord && animal.HeightCoordinate == targetheightcoord - 1)) HeightCoordinate -= 1;
                        return true;
                    }
                }
            return false;
        }
    }
}