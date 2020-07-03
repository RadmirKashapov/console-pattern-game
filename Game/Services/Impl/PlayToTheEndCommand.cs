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

        public PlayToTheEndCommand(Play play)
        {
            Playfield = play;
        }

        public void Execute()
        {
            firstBefore = Playfield.FirstPlayerArmy.Copy();
            secondBefore = Playfield.SecondPlayerArmy.Copy();
            Playfield.PlayToTheEnd();
            firstAfter = Playfield.FirstPlayerArmy.Copy();
            secondAfter = Playfield.SecondPlayerArmy.Copy();
        }

        public void Redo()
        {
            Playfield.FirstPlayerArmy = firstAfter;
            Playfield.SecondPlayerArmy = secondAfter;
        }

        public void Undo()
        {
            Playfield.FirstPlayerArmy = firstBefore;
            Playfield.SecondPlayerArmy = secondBefore;
        }
    }
}
