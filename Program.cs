namespace Savanna
{
    internal class Program
    {
        private static void Main()
        {
            Game.GameEngine game = new Game.GameEngine();
            GameTimer timer = new GameTimer();
            /*Lion a1 = new Lion() { WidthCoordinate = 0, HeightCoordinate = 0 };
            Lion a2 = new Lion() { WidthCoordinate = 1, HeightCoordinate = 0 };
            game.GameAnimals.Add(a1);
            game.GameAnimals.Add(a2);*/
            timer.StartTimer(game);
        }
    }
}