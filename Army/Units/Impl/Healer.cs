using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Healer : IUnit, ICloneable, ISpecialAction
    {
        public int Cost { get; }
        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }
        public int SpecialActionStrength { get; set; }

        public Healer(int cost, int hp, int ad, int df, int specialActionStrength)
        {
            this.Cost = cost;
            this.Hp = hp;
            this.Ad = ad;
            this.Df = df;
            this.SpecialActionStrength = specialActionStrength;
        }
        public Healer(Healer healer)
        {
            this.Cost = healer.Cost;
            this.Hp = healer.Hp;
            this.Ad = healer.Ad;
            this.Df = healer.Df;
            this.SpecialActionStrength = healer.SpecialActionStrength;
        }

        public object Clone()
        {
            return new Healer(this);
        }

        public void Heal(IUnit unit)
        {

            Random rnd = new Random();
            int magic = rnd.Next(0, 3);
            int heal = rnd.Next(0, unit.Hp);

            if (magic == 1 || magic == 2)
            {
                unit.Hp = heal;
            }
        }

        public IUnit DoSpecialAction(IUnit unit)
        {

            if(unit is IHealable)
            {
                Heal(unit);
            }

            return unit;
        }
    }
}
