using ConsoleGame.Army.Units.Impl;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units
{
    public interface IUnit : ICloneable
    {
        Defaults.UNITS UnitTypeId { get; set; }
        int Cost { get; set; } // "стоимость" создания
        int Hp { get; set; } // оставшаяся жизнь
        int Ad { get; set; } // сила атаки
        int Df { get; set; } // уровень защиты
        string Name { get; set; }

        IUnit TakeDamage(IUnit unit)
        {
            int Attack = Ad;

            switch (this.UnitTypeId)
            {
                case Defaults.UNITS.ARCHER:
                    if (unit is WanderingTownAdapter)
                        Attack = Defaults.WanderingTown.damageByArcher; 
                    break;

                case Defaults.UNITS.KNIGHT:

                    if (((IFashionable)this).Accessories != null && ((IFashionable)this).Accessories.ContainsKey((int)(Defaults.FASHIONABLE_ACCESSORIES.HORSE)))
                        if (unit is WanderingTownAdapter)
                        {
                            Attack = Defaults.WanderingTown.damageByKnightWithHorse;
                        }

                    break;
            }

            if(this is WanderingTownAdapter)
            {
                if (unit is WanderingTownAdapter)
                {
                    Random rnd = new Random();
                    Attack = rnd.Next(1, Defaults.WanderingTown.health);
                }
            }

            if (Attack >= unit.Hp + unit.Df) return null;

            if (unit.Df > Attack)
            {
                unit.Df -= Attack;

                return unit;
            }

            unit.Hp -= Attack - unit.Df;
            unit.Df = 0;

            return unit;

        }

        public string GetInfo()
        {
            var info = $"Юнит {Name}. Здоровье: {Hp}. Атака: {Ad}. Защита: {Df}\n";
            return info;
        }
    }
}
