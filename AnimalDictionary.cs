using System;
using System.Collections.Generic;
using System.Linq;

namespace Savanna
{
    public class AnimalDictionary
    {
        public Dictionary<char, Type> AnimalTypes { get; set; } = new Dictionary<char, Type>();

        public AnimalDictionary()
        {
            CreateDictionary();
        }

        private void CreateDictionary()
        {
            IEnumerable<Animals.Animal> animaltypes = typeof(Animals.Animal)
            .Assembly.GetTypes()
            .Where(t => t.IsSubclassOf(typeof(Animals.Animal)) && !t.IsAbstract)
            .Select(t => (Animals.Animal)Activator.CreateInstance(t));

            foreach (var type in animaltypes)
            {
                Animals.Animal animal = (Animals.Animal)Activator.CreateInstance(type.GetType());
                AnimalTypes.Add(animal.AnimalSymbol, type.GetType());
            }
        }
    }
}