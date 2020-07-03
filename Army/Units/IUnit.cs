using ConsoleGame.Army.Units.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units
{
    public interface IUnit : ICloneable
    {
        int Cost { get; set; } // "стоимость" создания
        int Hp { get; set; } // оставшаяся жизнь
        int Ad { get; set; } // сила атаки
        int Df { get; set; } // уровень защиты
        string Name { get; set; }

        virtual IUnit TakeDamage(IUnit unit)
        {
            if (Ad >= unit.Hp + unit.Df) return null;

            if (unit.Df > Ad) 
            {
                unit.Df -= Ad;

                return unit;
            }

            unit.Hp -= Ad - unit.Df;
            unit.Df = 0;

            return unit;

        }

        public virtual string GetInfo()
        {
            var info = $"Юнит {Name}. Здоровье: {Hp}. Атака: {Ad}. Защита: {Df}\n";
            return info;
        }

        public void DeathNotifier()
        {
            Console.WriteLine("Beeep");
        }
    }
}
