using System;
using System.Timers;

namespace Savanna
{
    internal class GameLoop
    {
        /// <summary>
        /// Creates game timer and loops each second meanwhile capturing user input 
        /// Each loop iterates game and prints game in the console
        /// </summary>
        public void StartTimer(Game.GameEngine game)
        {
            Timer timer = new Timer(1000);

            timer.Elapsed += (sender, e) => TimerTick(game);
            timer.Start();
            Console.Clear();
            game.PrintField();
            SavannaClassLibrary.UserInput input = new SavannaClassLibrary.UserInput();
            input.AddAnimal(game);
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