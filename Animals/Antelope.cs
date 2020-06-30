namespace Savanna.Animals

{
    internal class Antelope : Animal
    {
        public override char AnimalSymbol { get { return 'A'; } }

        public override double Health { get; set; } = 20;

        /// <summary>
        /// Specifies Antelopes logic on how it moves their priorities are Run From a Lion > Breed > Stray
        /// </summary>
        public override void Move(Game.GameEngine game)
        {
            if (RetreatFromALion(game)) { }
            else if (Breed(game)) { }
            else Stray(game);
            Health -= 0.5;
        }

        /// <summary>
        /// Function declarates how Antelope moves when running from a lion
        /// </summary>
        public bool RetreatFromALion(Game.GameEngine game)
        {
            int visionrange = 6;
            for (int widthvisionrange = -visionrange; widthvisionrange <= visionrange; widthvisionrange++)
                for (int heightvisionrange = -visionrange; heightvisionrange <= visionrange; heightvisionrange++)
                {
                    if (WidthCoordinate + widthvisionrange < 0 || WidthCoordinate + widthvisionrange >= game.Width || HeightCoordinate + heightvisionrange < 0 || HeightCoordinate + heightvisionrange >= game.Height) continue;
                    else
                        if (game.Field[WidthCoordinate + widthvisionrange, HeightCoordinate + heightvisionrange] == 'L')
                    {
                        int targetwidthcoord = WidthCoordinate + widthvisionrange;
                        int targetheightcoord = HeightCoordinate + heightvisionrange;

                        if (WidthCoordinate > 0)
                        {
                            if (targetwidthcoord > WidthCoordinate && WidthCoordinate > 0 && game.Field[WidthCoordinate - 1, HeightCoordinate] == ' ') WidthCoordinate -= 1;
                            else if (targetwidthcoord == WidthCoordinate && WidthCoordinate < game.Width - 1 && game.Field[WidthCoordinate - 1, HeightCoordinate] == ' ') WidthCoordinate -= 1;
                        }
                        else if (WidthCoordinate < game.Width - 1)
                        {
                            if (targetwidthcoord < WidthCoordinate && WidthCoordinate < game.Width - 1 && game.Field[WidthCoordinate + 1, HeightCoordinate] == ' ') WidthCoordinate += 1;
                            else if (targetwidthcoord == WidthCoordinate && WidthCoordinate < game.Width - 1 && game.Field[WidthCoordinate + 1, HeightCoordinate] == ' ') WidthCoordinate += 1;
                        }

                        if (HeightCoordinate > 0)
                        {
                            if (targetheightcoord > HeightCoordinate && HeightCoordinate > 0 && game.Field[WidthCoordinate, HeightCoordinate - 1] == ' ') HeightCoordinate -= 1;
                            else if (targetheightcoord == HeightCoordinate && HeightCoordinate < game.Width - 1 && game.Field[WidthCoordinate, HeightCoordinate - 1] == ' ') HeightCoordinate -= 1;
                        }
                        else if (HeightCoordinate < game.Height - 1)
                        {
                            if (targetheightcoord < HeightCoordinate && HeightCoordinate > game.Height - 1 && game.Field[WidthCoordinate, HeightCoordinate + 1] == ' ') HeightCoordinate += 1;
                            else if (targetheightcoord == HeightCoordinate && HeightCoordinate < game.Width - 1 && game.Field[WidthCoordinate, HeightCoordinate + 1] == ' ') HeightCoordinate += 1;
                        }
                        return true;
                    }
                }
            return false;
        }
    }
}