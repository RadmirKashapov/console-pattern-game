using ConsoleGame.Army.Units.Impl.Components;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Infantry : IUnit, ICloneable, ISpecialAction
    {
        public int Cost { get; set; }

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
                if(unit.Accessories.Count == 4)
                {
                    return null;
                }

                Random rnd = new Random();
                magic = rnd.Next(0, 4);

                if (unit.Accessories.ContainsKey(magic))
                {
                    continue;
                } 
                else {
                    flag = false;
                }
            }

            switch (magic)
            {
                case 0:
                    accessory = new ArmorComponent();
                    break;
                case 1:
                    accessory = new HelmetComponent();
                    break;
                case 2:
                    accessory = new HorseComponent();
                    break;
                case 3:
                    accessory = new PeakComponent();
                    break;
            }

            unit.Accessories.Add(magic, accessory.ToString());

            return accessory;
        }
    }
}
