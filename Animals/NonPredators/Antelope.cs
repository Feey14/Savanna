using AnimalTypeClassLibrary;

namespace Savanna.Animals.NonPredators
{
    public class Antelope : NonPredator
    {
        public override char AnimalSymbol => 'A';
        public override double Health { get; set; } = 20;
        public override int VisionRange => 6;
    }
}