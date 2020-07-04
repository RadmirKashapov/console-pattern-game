using ConsoleGame.Infrastructure;
using NetCoreAudio;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class UserArmy : IArmy
    {

        protected readonly object lockObj = new object();
        protected UnitFactory unitFactory { get; set; }
        private static Logger logger { get; set; }

        public string Name { get; set; }
        public int Money { get; set; }

        private List<IUnit> Units { get; set; }
        public UserArmy(int money)
        {
            unitFactory = UnitFactory.GetInstance();
            logger = Logger.GetInstance();
            Money = money;
            Units = new List<IUnit>();
        }

        public UserArmy(UserArmy army)
        {
            unitFactory = UnitFactory.GetInstance();
            logger = Logger.GetInstance();
            Name = army.Name;
            Money = army.Money;

            Units = new List<IUnit>();

            foreach (var elem in army.Units)
            {
                Units.Add((IUnit)elem.Clone());
            }
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
                int character = rnd.Next(0, Enum.GetNames(typeof(Defaults.UNITS)).Length);

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

        public void RemoveAllKilledUnits()
        {
            this.Units.RemoveAll(item => item == null);
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
            return new UserArmy(this);
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

        public void DeathNotifier()
        {
            //var player = new Player();
            //player.Play(@"C:\Users\mylif\source\repos\Projects\ConsoleGame\beep.wav");

            Console.Beep();

        }
    }
}
