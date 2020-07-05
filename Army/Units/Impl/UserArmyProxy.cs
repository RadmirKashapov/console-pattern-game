using ConsoleGame.Infrastructure;
using NetCoreAudio;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleGame.Army.Units.Impl
{
    class UserArmyProxy : IArmy
    {
        private UserArmy UserArmy { get; set; }
        private static Logger Logger { get; set; }
        public string Name { get; set; }
        public int Money { get; set; }

        public List<IUnit> Units { get; set; }

        public UserArmyProxy(UserArmy army)
        {
            Name = army.Name;
            Money = army.Money;
            UserArmy = army;
            Units = new List<IUnit>();
            Logger = Logger.GetInstance();

            CopyUnits(army);
        }

        private UserArmyProxy(UserArmyProxy army)
        {
            Name = army.Name;
            Money = army.Money;
            UserArmy = army.UserArmy;
            Logger = Logger.GetInstance();

            Units = new List<IUnit>();

            CopyUnits(army);
        }

        public IArmy Copy()
        {
            return new UserArmyProxy(this);
        }

        public void CreateArmy()
        {
            UserArmy.CreateArmy();
            CopyUnits(UserArmy);
        }

        public void DeathNotifier(string audioPath)
        {
            var audioPlayer = AudioPlayerFacade.GetInstance();
            audioPlayer.Play(@"C:\Users\mylif\source\repos\Projects\ConsoleGame\beep.wav");
        }

        public string GetInfo()
        {
            var info = $"{Name}\n";
            foreach (var unit in Units)
            {
                info += $"Юнит {unit.Name}. Здоровье: {unit.Hp}. Атака: {unit.Ad}. Защита: {unit.Df}\n";
            }

            return info;
        }

        public int IndexOf(IUnit unit)
        {
            return Units.IndexOf(unit);
        }

        public void Push(IUnit unit)
        {
            this.Units.Add(unit);
        }

        public int Remove(IUnit unit)
        {
            int index = Units.IndexOf(unit);
            Units.Remove(unit);
            Logger.Log($"Удалена игровая единица {unit} - {unit.Name}");
            return index;
        }

        public void SetBank(int money)
        {
            UserArmy.SetBank(money);
            Money = money;
        }

        public void SetName(string name)
        {
            UserArmy.SetName(name);
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

        public IUnit this[int i]
        {
            get
            {
                return Units[i];
            }
        }

        private void CopyUnits(IArmy army)
        {
            if (Units == null)
                Units = new List<IUnit>();

            foreach (var elem in army.Units)
            {
                Units.Add((IUnit)elem.Clone());
            }
        }
    }
}
