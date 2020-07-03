using ConsoleGame.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class UserArmy : IArmy
    {
        protected UnitFactory unitFactory { get; set; }
        private static Logger logger { get; set; }

        public string Name { get; set; }
        public int Money { get; set; }

        private List<IUnit> Units = new List<IUnit>();
        public UserArmy()
        {
            unitFactory = UnitFactory.GetInstance();
            logger = new Logger();
        }

        public void SetBank(int money)
        {
            Money = money;
        }

        public void SetName(string name)
        {
            Name = name;
        }

        public void CreateArmy()
        {

            Random rnd = new Random();
            while (Money > 0)
            {
                int character = rnd.Next(0, 5);

                var unit = unitFactory.CreateUnit(character, Money);

                if (unit == null)
                {
                    Money = 0;
                    logger.Log("Деньги кончились");
                    continue;
                }

                if (Money - unit.Cost >= 0)
                {
                    Money -= unit.Cost;
                    Units.Add(unit);
                    logger.Log($"Создана игровая единица {unit} - {unit.Name}. На счету осталось {Money} единиц денег.");
                }

            }
        }

        public int Count()
        {
            return Units.Count;
        }

        public int Remove(IUnit unit)
        {
            int index = Units.IndexOf(unit);
            Units.Remove(unit);
            logger.Log($"Удалена игровая единица {unit} - {unit.Name}");
            return index;
        }

        public bool IsEmpty()
        {
            return Units.Count == 0;
        }

        public IUnit this[int i]
        {
            get
            {
                return Units[i];
            }
        }

        public int IndexOf(IUnit unit)
        {
            return Units.IndexOf(unit);
        }

        public UserArmy Copy()
        {
            return (UserArmy)this.MemberwiseClone();
        }

        public void Push(IUnit unit)
        {
            this.Units.Add(unit);
        }

        public string GetInfo()
        {
            var info = $"{Name}\n";
            foreach(var unit in Units)
            {
                info += $"Юнит {unit.Name}. Здоровье: {unit.Hp}. Атака: {unit.Ad}. Защита: {unit.Df}\n";
            }

            return info;
        }
    }
}
