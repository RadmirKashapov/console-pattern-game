using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class Decorator: AccessoryDecorator
    {
        IUnit unit { get; set; }
        public Decorator(AccessoryComponent comp, IUnit unit) : base(comp)
        {
            this.unit = unit;
        }

        public override string AddAccessory()
        {
            unit.Df += 10;
            unit.Name +=  " " + base.AddAccessory();
            return unit.Name;
        }


        public IUnit GetUnit()
        {
            return this.unit;
        }
    }
}
