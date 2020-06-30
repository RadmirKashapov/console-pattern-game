using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Knight : IUnit, ICloneable
    {
        public int Cost { get; }

        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }

        public Knight(int cost, int hp, int ad, int df)
        {
            this.Cost = cost;
            this.Hp = hp;
            this.Ad = ad;
            this.Df = df;
        }
        public Knight(Knight knight)
        {
            this.Cost = knight.Cost;
            this.Hp = knight.Hp;
            this.Ad = knight.Ad;
            this.Df = knight.Df;
        }
        public object Clone()
        {
            return new Knight(this);
        }

    }
}
