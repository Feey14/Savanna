using System.Collections.Generic;

namespace Savanna.Animals
{
    public abstract class Animal
    {
        public int WidthCoordinate { get; set; }
        public int HeightCoordinate { get; set; }
        public abstract double Health { get; set; }
        public abstract char AnimalSymbol { get; }
        public abstract void Move(Game.GameEngine game);
        public void Die() 
        {
            Health = 0;   
        }
    }
}