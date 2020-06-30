using Savanna.Game;
using System;

namespace Savanna
{
    internal class UserInput
    {
        public void AddAnimal(GameEngine game)
        {
            ConsoleKey input;
            do
            {
                input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.A:
                        Animals.Antelope antelope = new Animals.Antelope();
                        game.AddAnimal(antelope);
                        break;

                    case ConsoleKey.L:
                        Animals.Lion lion = new Animals.Lion();
                        game.AddAnimal(lion);
                        break;

                    default:
                        break;
                }
            } while (input != ConsoleKey.Escape);
        }
    }
}