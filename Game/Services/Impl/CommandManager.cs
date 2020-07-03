using ConsoleGame.Army.Units.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Game.Services.Impl
{
    class CommandManager
    {
        private Stack<ICommand> StackUndo = new Stack<ICommand>();
        private Stack<ICommand> StackRedo = new Stack<ICommand>();
        private Play playfield;

        public CommandManager(UserArmy firstArmy, UserArmy secondArmy, IMode mode)
        {
            playfield = new Play(firstArmy, secondArmy, mode);
        }

        public void SetGameMode(IMode mode)
        {
            playfield.SetGameMode(mode);
        }

        public void Invoke(ICommand cmd)
        {
            cmd.Execute();
            StackUndo.Push(cmd);
            StackRedo.Clear();
        }

        public void Undo()
        {
            if (StackUndo.Count != 0)
            {
                ICommand cmd = StackUndo.Pop();
                cmd.Undo();
                StackRedo.Push(cmd);
            }
        }

        public void Redo()
        {
            if (StackRedo.Count != 0)
            {
                ICommand cmd = StackRedo.Pop();
                cmd.Redo();
                StackRedo.Push(cmd);
            }
        }

        public void Step()
        {
            Invoke(new OneStepCommand(playfield));
        }

        public void PlayToTheEnd()
        {
            Invoke(new PlayToTheEndCommand(playfield));
        }

        public string GetGameInfo()
        {
            return playfield.GetGameInfo();
        }

        public string GetStepInfo()
        {
            return playfield.GetStepInfo();
        }

    }
}
