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
        /// <summary>
        /// Deletes Animal from the game
        /// </summary>
        /// <param name="game"></param>
        public void Die(GameEngine game)
        {
            game.GameAnimals.Remove(this);
            game.Field[WidthCoordinate, HeightCoordinate] = ' ';
        }
        /// <summary>
        /// Function that makes animal stray in random direction if field is empty
        /// </summary>
        public void Stray(GameEngine game)
        {
            Random random = new Random();
            int x, y;
            do
            {
                x = WidthCoordinate + random.Next(2 + 1) - 1;
                y = HeightCoordinate + random.Next(2 + 1) - 1;
            } while (x < 0 || y < 0 || x >= game.Width || y >= game.Height || game.Field[x, y] != ' ');
            game.Field[WidthCoordinate, HeightCoordinate] = ' ';
            WidthCoordinate = x;
            HeightCoordinate = y;
            game.Field[WidthCoordinate, HeightCoordinate] = AnimalSymbol;
        }
        /// <summary>
        /// Breed function makes BabyAnimal and if parent are in the same position for 3 rounds animal is born
        /// </summary>
        public bool Breed(GameEngine game)
        {
            if (game.UnbornAnimals.Any(unbornanimal => unbornanimal.Parent1 == this || unbornanimal.Parent2 == this)) return true;
            if (WidthCoordinate > 0 && game.Field[WidthCoordinate - 1, HeightCoordinate] == AnimalSymbol)
            {
                var result = game.GameAnimals.Find(
                delegate (Animal an)
                {
                    return an.WidthCoordinate == WidthCoordinate - 1 && an.HeightCoordinate == HeightCoordinate;
                }
                );
                BabyAnimal testanimal = new BabyAnimal(this, result);
                testanimal.Move(game);
                game.UnbornAnimals.Add(testanimal);
                return true;
            }
            return false;
        }
    }
}