using System.Linq;

namespace Savanna.Animals

{
    internal class Antelope : Animal
    {
        public override char AnimalSymbol { get { return 'A'; } }

        public override double Health { get; set; } = 20;

        public override void Move(Game.GameEngine game)
        {
            if (RetreatFromALion(game)) { }
            else Stray(game);
            Health -= 0.5;
        }

        public bool RetreatFromALion(Game.GameEngine game)
        {
            int visionrange = 5;
            for (int widthvisionrange = -visionrange; widthvisionrange <= visionrange; widthvisionrange++)
                for (int heightvisionrange = -visionrange; heightvisionrange <= visionrange; heightvisionrange++)
                {
                    if (WidthCoordinate + widthvisionrange < 0 || WidthCoordinate + widthvisionrange >= game.Width || HeightCoordinate + heightvisionrange < 0 || HeightCoordinate + heightvisionrange >= game.Height) continue;
                    else
                        if (game.GameAnimals.Find(animal => animal.WidthCoordinate == WidthCoordinate +widthvisionrange && animal.HeightCoordinate == HeightCoordinate + heightvisionrange) is Lion)
                    {
                        int targetwidthcoord = WidthCoordinate + widthvisionrange;
                        int targetheightcoord = HeightCoordinate + heightvisionrange;
                        if (WidthCoordinate > 0 && WidthCoordinate < game.Width - 1)
                        {
                            if (targetwidthcoord > WidthCoordinate && !game.GameAnimals.Any(animal => animal.WidthCoordinate == WidthCoordinate -1 && animal.WidthCoordinate == HeightCoordinate)) WidthCoordinate -= 1;
                            else if (targetwidthcoord == WidthCoordinate && !game.GameAnimals.Any(animal => animal.WidthCoordinate == WidthCoordinate -1 && animal.WidthCoordinate == HeightCoordinate)) WidthCoordinate -= 1;
                            if (targetwidthcoord < WidthCoordinate && !game.GameAnimals.Any(animal => animal.WidthCoordinate == WidthCoordinate +1 && animal.WidthCoordinate == HeightCoordinate)) WidthCoordinate += 1;
                            else if (targetwidthcoord == WidthCoordinate && !game.GameAnimals.Any(animal => animal.WidthCoordinate == WidthCoordinate +1 && animal.WidthCoordinate == HeightCoordinate)) WidthCoordinate += 1;
                        }
                        if (HeightCoordinate > 0 && HeightCoordinate < game.Height - 1)
                        {
                            if (targetheightcoord > HeightCoordinate && !game.GameAnimals.Any(animal => animal.WidthCoordinate == WidthCoordinate && animal.HeightCoordinate == HeightCoordinate - 1)) HeightCoordinate -= 1;
                            else if (targetheightcoord == HeightCoordinate && !game.GameAnimals.Any(animal => animal.WidthCoordinate == WidthCoordinate && animal.HeightCoordinate == HeightCoordinate - 1)) HeightCoordinate -= 1;
                            if (targetheightcoord < HeightCoordinate && !game.GameAnimals.Any(animal => animal.WidthCoordinate == WidthCoordinate && animal.HeightCoordinate == HeightCoordinate + 1)) HeightCoordinate += 1;
                            else if (targetheightcoord == HeightCoordinate && !game.GameAnimals.Any(animal => animal.WidthCoordinate == WidthCoordinate && animal.HeightCoordinate == HeightCoordinate+1)) HeightCoordinate += 1;
                        }
                        return true;
                    }
                }
            return false;
        }
    }
}