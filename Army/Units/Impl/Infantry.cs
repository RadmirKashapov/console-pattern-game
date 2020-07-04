using ConsoleGame.Army.Units.Impl.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Infantry : IUnit, ICloneable, ISpecialAction, IHealable
    {
        public int Cost { get; set; }
        public Defaults.UNITS UnitTypeId { get; set; }
        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }
        public string Name { get; set; }
        public int Range { get ; set; }
        public int SpecialActionStrength { get; set; }

        public Infantry(int cost, int hp, int ad, int df, int specialActionStrength, int range)
        {
            this.Cost = cost;
            this.Hp = hp;
            this.Ad = ad;
            this.Df = df;
            this.SpecialActionStrength = specialActionStrength;
            this.Range = range;
            this.Name = Defaults.Infantry.name;
            this.UnitTypeId = Defaults.UNITS.INFANTRY;
        }

        public Infantry(Infantry infantry)
        {
            this.Cost = infantry.Cost;
            this.Hp = infantry.Hp;
            this.Ad = infantry.Ad;
            this.Df = infantry.Df;
            this.SpecialActionStrength = infantry.SpecialActionStrength;
            this.Range = infantry.Range;
            this.Name = infantry.Name;
            this.UnitTypeId = Defaults.UNITS.INFANTRY;
        }
        public object Clone()
        {
            return new Infantry(this);
        }

        public IUnit DoSpecialAction(IUnit unit)
        {
            
            if(unit is IFashionable)
            {
                Random rnd = new Random();

                AccessoryComponent accessory = GetAccessoryComponent((IFashionable)(unit));

                if (accessory == null)
                    return unit;

                var decorator = new Decorator(accessory, unit);
                var str = decorator.AddAccessory();

                return decorator.GetUnit();

            }

            return unit;
        }

        private AccessoryComponent GetAccessoryComponent(IFashionable unit)
        {
            bool flag = true;
            int magic = 0;
            AccessoryComponent accessory = null;

            if(unit.Accessories == null)
            {
                unit.Accessories = new Dictionary<int, string>();
            }

            while (flag)
            {
                if(unit.Accessories.Count == Enum.GetNames(typeof(Defaults.FASHIONABLE_ACCESSORIES)).Length)
                {
                    return null;
                }

                Random rnd = new Random();
                magic = rnd.Next(0, Enum.GetNames(typeof(Defaults.FASHIONABLE_ACCESSORIES)).Length);

                if (unit.Accessories.ContainsKey(magic))
                {
                    continue;
                } 
                else {
                    flag = false;
                }
            }

            switch ((Defaults.FASHIONABLE_ACCESSORIES)magic)
            {
                case Defaults.FASHIONABLE_ACCESSORIES.ARMOR:
                    accessory = new ArmorComponent();
                    break;
                case Defaults.FASHIONABLE_ACCESSORIES.HELMET:
                    accessory = new HelmetComponent();
                    break;
                case Defaults.FASHIONABLE_ACCESSORIES.HORSE:
                    accessory = new HorseComponent();
                    break;
                case Defaults.FASHIONABLE_ACCESSORIES.PEAK:
                    accessory = new PeakComponent();
                    break;
            }

            unit.Accessories.Add(magic, accessory.ToString());

            return accessory;
        }
    }
}
