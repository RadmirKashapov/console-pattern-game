using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Infantry : IUnit, ICloneable
    {
        public int Cost { get; }

        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }

        public Infantry(int cost, int hp, int ad, int df)
        {
            this.Cost = cost;
            this.Hp = hp;
            this.Ad = ad;
            this.Df = df;
        }

        public Infantry(Infantry infantry)
        {
            this.Cost = infantry.Cost;
            this.Hp = infantry.Hp;
            this.Ad = infantry.Ad;
            this.Df = infantry.Df;
        }
        public object Clone()
        {
            return new Infantry(this);
        }

    }
}
