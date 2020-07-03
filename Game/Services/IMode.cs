using ConsoleGame.Army.Units;
using ConsoleGame.Army.Units.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Game.Services
{
    interface IMode
    {
        public int rowSize { get; set; }
        public List<IUnit> GetArcherTargets(UserArmy firstArmy, UserArmy secondArmy, ISpecialAction unit)
        {
            var targets = new List<IUnit>();

            int col = (firstArmy.Count() - firstArmy.IndexOf((IUnit)unit) - 1) % rowSize;
            int row = (firstArmy.Count() - firstArmy.IndexOf((IUnit)unit) - 1) / rowSize;
            if (row >= unit.Range)
                return targets;
            for (int i = secondArmy.Count() - 1 - col, targetsCount = unit.Range - row; i >= 0 && targetsCount > 0; i -= rowSize, targetsCount--)
                targets.Add(secondArmy[i]);
            return targets;
        }

        public List<IUnit> GetDoctorTargets(UserArmy firstArmy, ISpecialAction unit)
        {
            var targets = new List<IUnit>();
            int index = firstArmy.IndexOf((IUnit)unit);

            int col = (firstArmy.Count() - index - 1) % rowSize; //колонна
            int row = (firstArmy.Count() - index - 1) / rowSize; //ряд

            for (int i = 0; i < firstArmy.Count(); i++)
            {
                int c = (firstArmy.Count() - i - 1) % rowSize;
                int r = (firstArmy.Count() - i - 1) / rowSize;
                if (Math.Sqrt((c - col) * (c - col) + (r - row) * (r - row)) <= unit.Range)
                    targets.Add(firstArmy[i]);
            }
            return targets;
        }

        public List<IUnit> GetInfantryTargets(UserArmy firstArmy, ISpecialAction unit)
        {
            var targets = new List<IUnit>();
            int index = firstArmy.IndexOf((IUnit)unit);

            int col = (firstArmy.Count() - index - 1) % rowSize; //колонна
            int row = (firstArmy.Count() - index - 1) / rowSize; //ряд

            for (int i = 0; i < firstArmy.Count(); i++)
            {
                int c = (firstArmy.Count() - i - 1) % rowSize;
                int r = (firstArmy.Count() - i - 1) / rowSize;
                if (Math.Sqrt((c - col) * (c - col) + (r - row) * (r - row)) <= unit.Range)
                {
                    if(firstArmy[i] is Proxy && ((Proxy)firstArmy[i]).Unit is IFashionable)
                      targets.Add(firstArmy[i]);
                }
            }
            return targets;
        }
    }
}
