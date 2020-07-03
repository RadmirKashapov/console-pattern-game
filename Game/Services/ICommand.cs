using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Game.Services
{
    interface ICommand
    {
        void Execute();
        void Undo();
        void Redo();
    }
}
