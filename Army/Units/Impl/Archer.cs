using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Archer : IUnit, ICloneable, ISpecialAction
    {
        public int Cost { get; set; }
        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }
        public int SpecialActionStrength { get ; set ; }
        public int Range { get; set; }

        public string Name { get; set; }

        public Archer(int cost, int hp, int ad, int df, int specialActionStrength, int range)
        {
            this.Cost = cost;
            this.Hp = hp;
            this.Ad = ad;
            this.Df = df;
            this.SpecialActionStrength = specialActionStrength;
            this.Range = range;
            this.Name = Defaults.Archer.name;
        }

        public Archer(Archer archer)
        {
            this.Cost = archer.Cost;
            this.Hp = archer.Hp;
            this.Ad = archer.Ad;
            this.Df = archer.Df;
            this.SpecialActionStrength = archer.SpecialActionStrength;
            this.Range = archer.Range;
            this.Name = archer.Name;
        }
        public object Clone()
        {
            return new Archer(this);
        }

        public IUnit DoSpecialAction(IUnit unit)
        {

            if (SpecialActionStrength > unit.Hp + unit.Df)
            {
                return null;
            }

            if (unit.Df > SpecialActionStrength)
            {
                unit.Df -= SpecialActionStrength;

                if (unit.Df < 0)
                    unit.Df = 0;

                return unit;
            }

            unit.Hp -= SpecialActionStrength - unit.Df;
            unit.Df = 0;
            return unit;
        }
    }
}
