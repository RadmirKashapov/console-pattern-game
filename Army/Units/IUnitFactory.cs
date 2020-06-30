using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units
{
    public interface IUnitFactory
    {
        IUnit CreateUnit(int id, params object[] args);
    }
}
