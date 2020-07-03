using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units
{
    public interface ISpecialAction
    {
        int Range { get; set; }
        int SpecialActionStrength { get; set; }
        IUnit DoSpecialAction(IUnit unit);
    }
}
