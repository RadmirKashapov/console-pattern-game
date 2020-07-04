using ConsoleGame.Army.Units.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Game.Services.Impl
{
    class PlayToTheEndCommand : ICommand
    {
        private Play Playfield { get; set; }
        private UserArmy firstBefore { get; set; }
        private UserArmy secondBefore { get; set; }
        private UserArmy firstAfter { get; set; }
        private UserArmy secondAfter { get; set; }

        private bool statusBefore { get; set; }
        private bool statusAfter { get; set; }

        public PlayToTheEndCommand(Play play)
        {
            Playfield = play;
        }

        public void Execute()
        {
            firstBefore = Playfield.FirstPlayerArmy.Copy();
            secondBefore = Playfield.SecondPlayerArmy.Copy();
            statusBefore = Playfield.GetGameStatus();
            Playfield.PlayToTheEnd();
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
