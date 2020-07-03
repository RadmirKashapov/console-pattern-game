using ConsoleGame.Army.Units;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Game.Services.Impl
{
    class OneToOneGameMode : IMode
    {
        public int rowSize { get; set; } 

        public OneToOneGameMode()
        {
            rowSize = 1;
        }
    }

    class ThreeToThreeGameMode : IMode
    {
        public int rowSize { get; set; }

        public ThreeToThreeGameMode()
        {
            rowSize = 3;
        }
    }

    class NToMGameMode : IMode
    {
        public int rowSize { get; set; }

        public NToMGameMode(int n)
        {
            rowSize = n;
        }
    }
}
