using System.Collections.Generic;

namespace Savanna.Animals
{
    public abstract class Animal
    {
        public int WidthCoordinate;
        public int HeightCoordinate;
        public abstract char AnimalSymbol { get; }

        public abstract void Move(Game.GameEngine game, List<Animal> newlist);
    }
}