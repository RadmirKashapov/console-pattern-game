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
            this.unit.Df += 10;
            this.unit.Name +=  " " + base.AddAccessory();
            return this.unit.Name;
        }


        public IUnit GetUnit()
        {
            return this.unit;
        }
    }
}
