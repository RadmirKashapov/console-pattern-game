using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units
{
    class AccessoryDecorator: AccessoryComponent
    {
        protected AccessoryComponent _component;

        public AccessoryDecorator(AccessoryComponent component)
        {
            this._component = component;
        }

        public void SetComponent(AccessoryComponent component)
        {
            this._component = component;
        }


        public override string AddAccessory()
        {
            if (this._component != null)
            {
                return this._component.AddAccessory();
            }
            else
            {
                return string.Empty;
            }
        }
    }
}
