using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units
{
    public interface IUnit : ICloneable
    {
        int Cost { get; } // "стоимость" создания
        int Hp { get; set; } // оставшаяся жизнь
        int Ad { get; set; } // сила атаки
        int Df { get; set; } // уровень защиты

        virtual IUnit TakeDamage(IUnit unit)
        {
            if (Ad > unit.Hp + unit.Df) return null;

            if (unit.Df > Ad) 
            {
                unit.Df -= Ad;
                if (unit.Df < 0)
                    unit.Df = 0;

                return unit;
            }

            unit.Hp -= Ad - unit.Df;
            unit.Df = 0;
            return unit;

        }
    }
}
