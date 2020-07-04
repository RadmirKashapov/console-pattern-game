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

        private int UndoCount
        {
            get
            {
                return StackUndo.Count;
            }
        }

        private int RedoCount
        {
            get
            {
                return StackRedo.Count;
            }
        }

        private Play playfield { get; set; }

        public CommandManager(Play play)
        {
            playfield = play;
        }

        public void SetGameMode(string answer)
        {
            IMode mode = null;

            switch (answer)
            {
                case "1":

                    mode = new OneToOneGameMode();
                    break;

                case "2":

                    mode = new ThreeToThreeGameMode();
                    break;

                case "3":

                    mode = new NToMGameMode(Math.Min(playfield.FirstPlayerArmy.Count(), playfield.SecondPlayerArmy.Count()));
                    break;
            }

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
            if (UndoCount != 0)
            {
                ICommand cmd = StackUndo.Pop();
                cmd.Undo();
                StackRedo.Push(cmd);
            }
        }

        public void Redo()
        {
            if (RedoCount != 0)
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

    }
}
