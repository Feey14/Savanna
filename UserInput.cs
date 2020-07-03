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
            AnimalDictionary animaldictionary = new AnimalDictionary();
            ConsoleKeyInfo input;
            do
            {
                input = Console.ReadKey();
                if (animaldictionary.AnimalTypes.TryGetValue(input.KeyChar, out Type type))
                {
                    Animals.Animal animal = (Animals.Animal)Activator.CreateInstance(type);
                    game.AddAnimal(animal);
                }
            } while (input.Key != ConsoleKey.Escape);
        }
    }
}