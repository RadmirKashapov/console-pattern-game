using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Game.Services
{
    interface IGame
    {
        void Turn();
        void TurnToEnd();
    }
}
