using System;
using System.Timers;

namespace Savanna
{
    internal class GameTimer
    {
        public void StartTimer(Game.GameEngine game)
        {
            Timer timer = new Timer(1000);

            timer.Elapsed += (sender, e) => TimerTick(game);
            timer.Start();
            Console.Clear();
            game.PrintField();
            ConsoleKey input;
            do
            {
                input = Console.ReadKey().Key;
                switch (input)
                {
                    case ConsoleKey.A:
                        Animals.Antelope antelope = new Animals.Antelope();
                        game.AddAnimal(antelope);
                        break;

                    case ConsoleKey.L:
                        Animals.Lion lion = new Animals.Lion();
                        game.AddAnimal(lion);
                        break;

                    default:
                        break;
                }
            } while (input != ConsoleKey.Escape);
            Console.ReadLine();
            timer.Stop();
            timer.Dispose();
        }

        private void TimerTick(Game.GameEngine game)
        {
            game.Iterate();
            Console.Clear();
            game.PrintField();
        }
    }
}