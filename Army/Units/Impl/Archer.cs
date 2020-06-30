using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Archer : IUnit, ICloneable, ISpecialAction
    {
        public int Cost { get; }
        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }
        public int SpecialActionStrength { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public Archer(int cost, int hp, int ad, int df)
        {
            this.Cost = cost;
            this.Hp = hp;
            this.Ad = ad;
            this.Df = df;
        }

        public Archer(Archer archer)
        {
            this.Cost = archer.Cost;
            this.Hp = archer.Hp;
            this.Ad = archer.Ad;
            this.Df = archer.Df;
        }
        public object Clone()
        {
            return new Archer(this);
        }

        public IUnit DoSpecialAction(IUnit unit)
        {
            throw new NotImplementedException();
        }
    }
}
