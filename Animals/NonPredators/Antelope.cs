namespace Savanna.Animals.NonPredators
{
    public class Antelope : NonPredator
    {
        public override char AnimalSymbol { get { return 'A'; } }
        public override double Health { get; set; } = 20;
        public override int VisionRange { get; set; } = 6;
    }
}