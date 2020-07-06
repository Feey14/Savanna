using AnimalsClassLibrary;
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
            //Assembly.Load("AnimalsClassLibrary");
            CreateDictionary();
        }

        /// <summary>
        /// Creates Dictionary with their symbol and Type used for creating new animals
        /// </summary>
        private void CreateDictionary()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(t => t.GetTypes()).Where(t => t.IsSubclassOf(typeof(Animal)) && !t.IsAbstract).Select(t => (Animal)Activator.CreateInstance(t));

            foreach (var type in types)
            {
                Animal animal = (Animal)Activator.CreateInstance(type.GetType());
                AnimalTypes.Add(animal.AnimalSymbol, type.GetType());
            }
        }
    }
}