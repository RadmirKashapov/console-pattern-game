using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class WanderingTown
    {
        public string Name { get; set; }
        public int Defence { get; set; }

        public WanderingTown()
        {
            Name = "Гуляй-город";
            Defence = 100;
        }
    }
}
