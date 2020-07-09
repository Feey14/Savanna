using AnimalTypeClassLibrary;

namespace AnimalsClassLibrary
{
    internal class Tiger : Predator
    {
        public override char AnimalSymbol => 'T';
        public override double Health { get; set; } = 20;
        public override int VisionRange => 6;
    }
}