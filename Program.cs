﻿using System;
using Savanna.Animals;
using System.Collections.Generic;
using System.Linq;

namespace Savanna
{
    internal class Program
    {
        private static void Main()
        {
            Game.GameEngine game = new Game.GameEngine();
            GameLoop timer = new GameLoop();
/*            Animals.NonPredators.Antelope a1 = new Animals.NonPredators.Antelope() { WidthCoordinate = 0, HeightCoordinate = 0 };
            Animals.NonPredators.Antelope a2 = new Animals.NonPredators.Antelope() { WidthCoordinate = 1, HeightCoordinate = 0 };
            game.GameAnimals.Add(a1);
            game.GameAnimals.Add(a2);*/
            timer.StartTimer(game);
        }
    }
}