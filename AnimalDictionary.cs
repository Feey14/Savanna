using AnimalTypeClassLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Savanna
{
    public class AnimalDictionary
    {
        public Dictionary<char, Type> AnimalTypes { get; set; } = new Dictionary<char, Type>();

        /// <summary>
        /// Loads assembly from dll and Creates Dicctionary with symbols and Types of animals
        /// </summary>
        public AnimalDictionary()
        {
            Assembly.LoadFrom("AnimalsClassLibrary.dll");
            CreateDictionary();
        }

        /// <summary>
        /// Creates Dictionary with their symbol and Type used for creating new animals
        /// </summary>
        private void CreateDictionary()
        {
            IEnumerable<Animal> types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes()).Where(t => t.IsSubclassOf(typeof(Animal)) && !t.IsAbstract).Select(t => (Animal)Activator.CreateInstance(t));

            foreach (Animal type in types)
            {
                Animal animal = (Animal)Activator.CreateInstance(type.GetType());
                AnimalTypes.Add(animal.AnimalSymbol, type.GetType());
            }
        }
    }
}