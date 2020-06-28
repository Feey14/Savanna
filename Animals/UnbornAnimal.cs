using System;
using System.Runtime.Remoting;
using System.Reflection;
using System.Text;

namespace Savanna.Animals

{
    internal class UnbornAnimal
    {
        private int roundCount { get; set; }
        public Animal parent1 { get; set; }
        private Animal parent2 { get; set; }

        public void Move(Game.GameEngine game)
        {
            var classname = parent1.GetType();
            Animal an = (Animal)Activator.CreateInstance(classname);
            an.WidthCoordinate = 0;
            an.HeightCoordinate = 0;
            game.AddAnimal(an);
        }
    }
}