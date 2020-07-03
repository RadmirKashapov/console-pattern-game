using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Wizard : IUnit, ICloneable, ISpecialAction
    {
        public int Cost { get; set; }

        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }
        public int SpecialActionStrength { get ; set; }
        public int Range { get; set; }
        public string Name { get; set; }

        public Wizard(int cost, int hp, int ad, int df, int specialActionStrength, int range)
        {
            this.Cost = cost;
            this.Hp = hp;
            this.Ad = ad;
            this.Df = df;
            this.SpecialActionStrength = specialActionStrength;
            this.Range = range;
            this.Name = Defaults.Wizard.name;
        }
        public Wizard(Wizard wizard)
        {
            this.Cost = wizard.Cost;
            this.Hp = wizard.Hp;
            this.Ad = wizard.Ad;
            this.Df = wizard.Df;
            this.SpecialActionStrength = wizard.SpecialActionStrength;
            this.Range = wizard.Range;
            this.Name = wizard.Name;
        }

        public object Clone()
        {
            return new Wizard(this);
        }

        public IUnit DoSpecialAction(IUnit unit)
        {
            if (unit is ICloneable && (unit is Archer || unit is Infantry))
            {
                Random rnd = new Random();
                int magic = rnd.Next(1, 3);
                if (magic == 1 || magic == 2)
                {
                    return (IUnit)unit.Clone();
                }
                else return null;
            }
            return null;

        }
    }
}
