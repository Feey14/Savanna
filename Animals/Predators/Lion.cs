using AnimalTypeClassLibrary;

namespace Savanna.Animals.Predators
{
    public class Lion : Predator
    {
        public override char AnimalSymbol { get { return 'L'; } }
        public override double Health { get; set; } = 20;
        public override int VisionRange => 6;
    }
}