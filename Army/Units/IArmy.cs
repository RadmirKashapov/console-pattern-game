using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units
{
    public interface IArmy
    {
        List<IUnit> CreateArmy(IUnitFactory factory);
    }
}
