using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Wizard : IUnit, ICloneable, ISpecialAction
    {
        public int Cost { get; }

        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }
        public int SpecialActionStrength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Wizard(int cost, int hp, int ad, int df)
        {
            this.Cost = cost;
            this.Hp = hp;
            this.Ad = ad;
            this.Df = df;
        }
        public Wizard(Wizard wizard)
        {
            this.Cost = wizard.Cost;
            this.Hp = wizard.Hp;
            this.Ad = wizard.Ad;
            this.Df = wizard.Df;
        }
        public object Clone()
        {
            return new Wizard(this);
        }

        public IUnit DoSpecialAction(IUnit unit)
        {
            if (unit is ICloneable)
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
