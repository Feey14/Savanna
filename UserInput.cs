
using Savanna.Game;
using System;

namespace Savanna
{
    public class UserInput
    {
        /// <summary>
        /// Captures user input key and adds animal that animal based on key input
        /// </summary>
        public void AddAnimal(GameEngine game)
        {
            ConsoleKey input;
           do
           {
                input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.A:
                        game.AddAnimal(new Animals.NonPredators.Antelope());
                        break;

                    case ConsoleKey.L:
                        game.AddAnimal(new Animals.Predators.Lion());
                        break;

                    default:
                        break;
                }
            } while (input != ConsoleKey.Escape);
        }
    }
}