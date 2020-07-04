using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Healer : IUnit, ICloneable, ISpecialAction, IHealable
    {
        public Defaults.UNITS UnitTypeId { get; set; }
        public int Cost { get; set; }
        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }
        public int SpecialActionStrength { get; set; }
        public int Range { get; set; }
        public string Name { get; set; }

        public Healer(int cost, int hp, int ad, int df, int specialActionStrength, int range)
        {
            this.Cost = cost;
            this.Hp = hp;
            this.Ad = ad;
            this.Df = df;
            this.SpecialActionStrength = specialActionStrength;
            this.Range = range;
            this.Name = Defaults.Healer.name;
            this.UnitTypeId = Defaults.UNITS.HEALER;
        }
        public Healer(Healer healer)
        {
            this.Cost = healer.Cost;
            this.Hp = healer.Hp;
            this.Ad = healer.Ad;
            this.Df = healer.Df;
            this.SpecialActionStrength = healer.SpecialActionStrength;
            this.Range = healer.Range;
            this.Name = healer.Name;
            this.UnitTypeId = Defaults.UNITS.HEALER;
        }

        public object Clone()
        {
            return new Healer(this);
        }

        private void Heal(IUnit unit)
        {

            Random rnd = new Random();
            int heal = rnd.Next(0, 100);
            
            unit.Hp += heal;

            if (unit.Hp > 100)
                unit.Hp = 100;
        }

        public IUnit DoSpecialAction(IUnit unit)
        {

            if(unit is IHealable)
            {
                Heal(unit);
                return unit;
            }

            return unit;
        }
    }
}
