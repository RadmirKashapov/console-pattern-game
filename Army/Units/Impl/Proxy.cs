using NetCoreAudio;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Proxy : IUnit
    {
        public Defaults.UNITS UnitTypeId { get; set; }
        public int Cost { get; set; }
        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }
        public string Name { get ; set; }

        public IUnit Unit { get; set; }

        public Proxy(IUnit unit)
        {
            Cost = unit.Cost;
            Hp = unit.Hp;
            Ad = unit.Ad;
            Df = unit.Df;
            Name = unit.Name;
            Unit = unit;
            UnitTypeId = unit.UnitTypeId;
        }

        public object Clone()
        {
            return Unit.Clone();
        }

        public void DeathNotifier()
        {
            //var player = new Player();
            //player.Play(@"C:\Users\mylif\source\repos\Projects\ConsoleGame\beep.wav");
            Console.Beep();
        }

        public IUnit DoSpecialAction(IUnit unit)
        {
            if (Unit is ISpecialAction) {
                return new Proxy(((ISpecialAction)Unit).DoSpecialAction(((Proxy)unit).Unit));
            }

            return unit;
        }

        public bool IsKnight()
        {
            return Unit is Knight;
        }
    }
}
