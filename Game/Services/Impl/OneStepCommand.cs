using ConsoleGame.Army.Units;
using ConsoleGame.Army.Units.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Game.Services.Impl
{
    class OneStepCommand : ICommand
    {
        private Play Playfield { get; set; }
        private IArmy firstBefore { get; set; }
        private IArmy secondBefore { get; set; }
        private IArmy firstAfter { get; set; }
        private IArmy secondAfter { get; set; }

        private bool statusBefore { get; set; }
        private bool statusAfter { get; set; }

        public OneStepCommand(Play play)
        {
            Playfield = play;
        }


        public void Execute()
        {
            firstBefore = Playfield.FirstPlayerArmy.Copy();
            secondBefore = Playfield.SecondPlayerArmy.Copy();
            statusBefore = Playfield.GetGameStatus();
            Playfield.Step();
            firstAfter = Playfield.FirstPlayerArmy.Copy();
            secondAfter = Playfield.SecondPlayerArmy.Copy();
            statusAfter = Playfield.GetGameStatus();
        }

        public void Redo()
        {
            Playfield.FirstPlayerArmy = firstAfter;
            Playfield.SecondPlayerArmy = secondAfter;
            Playfield.SetGameStatus(statusAfter);
        }

        public void Undo()
        {
            Playfield.FirstPlayerArmy = firstBefore;
            Playfield.SecondPlayerArmy = secondBefore;
            Playfield.SetGameStatus(statusBefore);
        }
    }
}
