using System;
using System.Collections.Generic;
using System.Linq;
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

        static readonly Dictionary<int, IUnit> _unitsDict = new Dictionary<int, IUnit>();
        static Dictionary<int, bool> _isMoneyEnoughDict = new Dictionary<int, bool>();

        public IUnit CreateUnit(int id, int money)
        {
            RecalculateMoneyFlags(money);
            if (!IsMoneyEnough())
                return null;

            if (_unitsDict.TryGetValue(id, out IUnit type))
                return (IUnit)type.Clone();

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register<Tderived>(int id) where Tderived : IUnit
        {
            var type = typeof(Tderived);

            if (type.IsInterface || type.IsAbstract)
                throw new ArgumentException("...");

            _unitsDict.Add(id, (IUnit)type);

            RegisterFlags(id);
        }

        private static void RegisterFlags(int id)
        {
            _isMoneyEnoughDict.Add(id, true);
        }

        private static bool IsMoneyEnough()
        {
            bool result = false;

            foreach(var flag in _isMoneyEnoughDict)
            {
                result = result || flag.Value;
            }

            return result;
        }

        private static void RecalculateMoneyFlags(int money)
        {
            _isMoneyEnoughDict = _isMoneyEnoughDict.Select(v => new KeyValuePair<int, bool>(v.Key, _unitsDict[v.Key].Cost >= money)).ToDictionary(v => v.Key, v=> v.Value);
        }
    }
}
