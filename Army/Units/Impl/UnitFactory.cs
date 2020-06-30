using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    enum UnitType
    {
        INFANTRY, KNIGHT, HEALER, ARCHER, WIZARD
    }
    class UnitFactory: IUnitFactory
    {
        private UnitFactory() { }

        static readonly Dictionary<int, IUnit> _dict = new Dictionary<int, IUnit>();

        public IUnit CreateUnit(int id, params object[] args)
        {
            IUnit type = null;
            if (_dict.TryGetValue(id, out type))
                return (IUnit)type.Clone();

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register<Tderived>(int id) where Tderived : IUnit
        {
            var type = typeof(Tderived);

            if (type.IsInterface || type.IsAbstract)
                throw new ArgumentException("...");

            _dict.Add(id, (IUnit)type);
        }
    }
}
