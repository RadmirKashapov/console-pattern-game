using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{

    public enum UNITS
    {
        INFANTRY, ARCHER, HEALER, KNIGHT, WIZARD
    }

    class Army : IArmy
    {
        protected int Money { get; set; }
        public Army(int money)
        {
            Money = money;
        }

        public List<IUnit> CreateArmy(IUnitFactory factory)
        {
            List<IUnit> Units = new List<IUnit>();

            Random rnd = new Random();
            while (this.Money > 0)
            {
                int character = rnd.Next(1, 5);

                var unit = factory.CreateUnit(character, this.Money);

                if (unit == null)
                {
                    this.Money = 0;
                    continue;
                }

                if (this.Money >= unit.Cost)
                {
                    this.Money -= unit.Cost;
                    Units.Add(unit);
                }
            }

            return Units;
        }
    }
}
