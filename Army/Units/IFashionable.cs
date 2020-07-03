using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units
{
    interface IFashionable
    {
        Dictionary<int, string> Accessories { get; set; }
    }
}
