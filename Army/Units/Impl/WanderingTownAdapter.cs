using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    class WanderingTownAdapter: IUnit
    {
        public Defaults.UNITS UnitTypeId { get; set; }
        public int Cost { get; set; }
        public int Hp { get; set; }
        public int Ad { get; set; }
        public int Df { get; set; }
        public string Name { get; set; }

        private readonly WanderingTown _wanderingTown;
        public WanderingTownAdapter(WanderingTown wanderingTown)
        {
            _wanderingTown = wanderingTown;
            Cost = Defaults.WanderingTown.price;
            Name = wanderingTown.Name;
            Df = wanderingTown.Defence;
            Hp = Defaults.WanderingTown.health;
            Ad = Defaults.WanderingTown.attack;
        }

        public WanderingTownAdapter(WanderingTownAdapter adapter)
        {
            _wanderingTown = adapter._wanderingTown;
            Cost = Defaults.WanderingTown.price;
            Name = adapter.Name;
            Df = adapter.Df;
            Hp = Defaults.WanderingTown.health;
            Ad = Defaults.WanderingTown.attack;
        }


        public object Clone()
        {
            return new WanderingTownAdapter(this);
        }
    }
}
