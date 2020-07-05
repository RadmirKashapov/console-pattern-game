using ConsoleGame.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Army.Units.Impl
{
    class UserArmy : IArmy
    {

        protected readonly object lockObj = new object();
        private static Logger Logger { get; set; }

        public string Name { get; set; }
        public int Money { get; set; }

        public List<IUnit> Units { get; set; }
        protected UnitFactory UnitFactory { get; set; }

        public UserArmy(int money)
        {
            UnitFactory = UnitFactory.GetInstance();
            Logger = Logger.GetInstance();
            Money = money;
            Units = new List<IUnit>();
        }

        public UserArmy(UserArmy army)
        {
            UnitFactory = UnitFactory.GetInstance();
            Logger = Logger.GetInstance();
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

        public int Count()
        {
            return Units.Count;
        }

        public bool IsEmpty()
        {
            return Units.Count == 0;
        }

        public void CreateArmy()
        {

            Random rnd = new Random();
            while (Money > 0)
            {
                int character = rnd.Next(0, Enum.GetNames(typeof(Defaults.UNITS)).Length);

                var unit = UnitFactory.CreateUnit(character, Money);

                if (unit == null)
                {
                    Money = 0;
                    Logger.Log("Деньги кончились");
                    continue;
                }

                if (Money - unit.Cost >= 0)
                {
                    Money -= unit.Cost;
                    Units.Add(unit);
                    Logger.Log($"Создана игровая единица {unit} - {unit.Name}. На счету осталось {Money} единиц денег.");
                }

            }
        }

        public int Remove(IUnit unit)
        {
            int index = Units.IndexOf(unit);
            Units.Remove(unit);
            Logger.Log($"Удалена игровая единица {unit} - {unit.Name}");
            return index;
        }

        public int IndexOf(IUnit unit)
        {
            return Units.IndexOf(unit);
        }

        public IArmy Copy()
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

        public void DeathNotifier(string audioPath)
        {

            //var player = new Player();
            //player.Play(@"C:\Users\mylif\source\repos\Projects\ConsoleGame\beep.wav")
           
            lock (lockObj)
            {
              
                Task.Factory.StartNew(() =>Console.Beep()); 
            }

        }

        public IUnit this[int i]
        {
            get
            {
                return Units[i];
            }
        }
    }
}
