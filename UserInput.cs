using Savanna.Animals;
using Savanna.Game;
using System;

namespace Savanna
{
    internal class UserInput
    {
        private static void AddAnimalToTheGame(Animal animal, GameEngine game)
        {
            game.AddAnimal(animal);
        }

        public void AddAnimal(GameEngine game)
        {
            ConsoleKey input;
            do
            {
                input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.A:
                        AddAnimalToTheGame(new Antelope(), game);
                        break;

                    case ConsoleKey.L:
                        AddAnimalToTheGame(new Lion(), game);
                        break;

                    default:
                        break;
                }
            } while (input != ConsoleKey.Escape);
        }
    }
}