using AnimalTypeClassLibrary;

namespace Savanna.Animals.NonPredators
{
    /// <summary>
    /// Creating Antelope that inherits from NonPredator class
    /// setting Antelope's health to 20 and vision range to 6
    /// </summary>
    public class Antelope : NonPredator
    {
        public override char AnimalSymbol => 'A';
        public override double Health { get; set; } = 20;
        public override int VisionRange => 6;
    }
}