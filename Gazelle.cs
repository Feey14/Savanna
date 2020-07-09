using AnimalTypeClassLibrary;

namespace AnimalsClassLibrary
{
    public class Gazelle : NonPredator
    {
        public override char AnimalSymbol => 'G';
        public override double Health { get; set; } = 20;
        public override int VisionRange => 6;
    }
}