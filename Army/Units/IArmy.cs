using ConsoleGame.Army.Units.Impl;
using ConsoleGame.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units
{
    public interface IArmy
    {
        public string Name { get; set; }
        public int Money { get; set; }

        public List<IUnit> Units { get; set; }


        public void CreateArmy();

        public void SetBank(int money);

        public void SetName(string name);

        public int Count();

        public int Remove(IUnit unit);

        public bool IsEmpty();

        public IUnit this[int i] { get; }

        int IndexOf(IUnit unit);

        IArmy Copy();

        void Push(IUnit unit);

        string GetInfo();

        void DeathNotifier(string audioPath);
    }
}
