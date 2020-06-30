namespace Savanna.Animals
{
    internal class Lion : Animal
    {
        public override char AnimalSymbol { get { return 'L'; } }
        public override double Health { get; set; } = 20;
        /// <summary>
        /// Defines Lion logic and how they move
        /// Eat Antelipe>ChaseAntelope>Breed>Stray
        /// </summary>

        public override void Move(Game.GameEngine game)
        {
            if (EatNearbyAntelope(game)) { }
            else if (ChaseNearbyAntelope(game)) { }
            else if (Breed(game)) { }
            else Stray(game);
            Health -= 0.5;
        }

        /// <summary>
        /// Checks for narbyAntelope
        /// Defines What happens when antelope is eaten
        /// </summary>
        public bool EatNearbyAntelope(Game.GameEngine game)
        {
            int visionrange = 1;
            for (int widthvisionrange = -visionrange; widthvisionrange <= visionrange; widthvisionrange++)
                for (int heightvisionrange = -visionrange; heightvisionrange <= visionrange; heightvisionrange++)
                    if (WidthCoordinate + widthvisionrange < 0 || WidthCoordinate + widthvisionrange >= game.Width || HeightCoordinate + heightvisionrange < 0 || HeightCoordinate + heightvisionrange >= game.Height) continue;
                    else
                        if (game.Field[WidthCoordinate + widthvisionrange, HeightCoordinate + heightvisionrange] == 'A')
                    {
                        Animal result = game.GameAnimals.Find(
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
                            game.Field[WidthCoordinate, HeightCoordinate] = 'L';
                        }
                        return true;
                    }
            return false;
        }

        /// <summary>
        /// Defines How Lion moves when chasing antelope
        /// </summary>
        public bool ChaseNearbyAntelope(Game.GameEngine game)
        {
            int visionrange = 6;
            for (int widthvisionrange = -visionrange; widthvisionrange <= visionrange; widthvisionrange++)
                for (int heightvisionrange = -visionrange; heightvisionrange <= visionrange; heightvisionrange++)
                {
                    if (WidthCoordinate + widthvisionrange < 0 || WidthCoordinate + widthvisionrange >= game.Width || HeightCoordinate + heightvisionrange < 0 || HeightCoordinate + heightvisionrange >= game.Height) continue;
                    else
                        if (game.Field[WidthCoordinate + widthvisionrange, HeightCoordinate + heightvisionrange] == 'A')
                    {
                        int targetwidthcoord = WidthCoordinate + widthvisionrange;
                        int targetheightcoord = HeightCoordinate + heightvisionrange;
                        if (targetwidthcoord > WidthCoordinate && game.Field[WidthCoordinate + 1, HeightCoordinate] == ' ') WidthCoordinate += 1;
                        else if (targetwidthcoord < WidthCoordinate && game.Field[WidthCoordinate - 1, HeightCoordinate] == ' ') WidthCoordinate -= 1;
                        if (targetheightcoord > HeightCoordinate && game.Field[WidthCoordinate, HeightCoordinate + 1] == ' ') HeightCoordinate += 1;
                        else if (targetheightcoord < HeightCoordinate && game.Field[WidthCoordinate, HeightCoordinate - 1] == ' ') HeightCoordinate -= 1;
                        return true;
                    }
                }
            return false;
        }
    }
}