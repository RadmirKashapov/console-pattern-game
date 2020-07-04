using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Knight : IUnit, ICloneable, IFashionable, IHealable
    {
        public int Cost { get; set; }
        public Defaults.UNITS UnitTypeId { get; set; }
        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }
        public string Name { get; set; }
        public Dictionary<int, string> Accessories { get; set; }

        public Knight(int cost, int hp, int ad, int df)
        {
            this.Cost = cost;
            this.Hp = hp;
            this.Ad = ad;
            this.Df = df;
            this.Name = Defaults.Knight.name;
            this.UnitTypeId = Defaults.UNITS.KNIGHT;
        }
        public Knight(Knight knight)
        {
            this.Cost = knight.Cost;
            this.Hp = knight.Hp;
            this.Ad = knight.Ad;
            this.Df = knight.Df;
            this.Name = knight.Name;
            this.UnitTypeId = Defaults.UNITS.KNIGHT;
        }
        public object Clone()
        {
            return new Knight(this);
        }

    }
}
