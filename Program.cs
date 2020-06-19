namespace Savanna
{
    internal class Program
    {
        private static void Main()
        {
            Game.GameEngine game = new Game.GameEngine();
            GameTimer timer = new GameTimer();
            timer.StartTimer(game);
        }
    }
}