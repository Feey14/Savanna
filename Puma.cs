using AnimalTypeClassLibrary;

namespace AnimalsClassLibrary
{
    internal class Puma : Predator
    {
        public override char AnimalSymbol => 'P';
        public override double Health { get; set; } = 20;
        public override int VisionRange => 6;
    }
}