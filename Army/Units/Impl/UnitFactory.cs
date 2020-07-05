using ConsoleGame.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    public class UnitFactory : IUnitFactory
    {
        private UnitFactory() { }

        private static UnitFactory unitFactory { get; set; }

        private static readonly Dictionary<int, Func<IUnit>> _unitsDict = new Dictionary<int, Func<IUnit>>();
        private static Dictionary<int, bool> _isMoneyEnoughDict = new Dictionary<int, bool>();

        private static Logger logger { get; set; }

        public static UnitFactory GetInstance()
        {
            if (unitFactory == null)
            {
                unitFactory = new UnitFactory();
                logger = Logger.GetInstance();
                unitFactory.RegisterUnits();
            }
            return unitFactory;
        }

        public IUnit CreateUnit(int id, int money)
        {
            RecalculateMoneyFlags(money);
            if (!IsMoneyEnough())
                return null;

            Func<IUnit> constructor = null;
            if (_unitsDict.TryGetValue(id, out constructor))
            {
                var obj = constructor();
                return obj;
            }

            throw new ArgumentException("No type registered for this id");
        }

        public static void Register(int id, Func<IUnit> ctor)
        {
            if (!_unitsDict.ContainsKey(id))
            {
                _unitsDict.Add(id, ctor);
                RegisterFlags(id);

                logger.Log($"Зарегистрирован объект {ctor.ToString()} с id {id}");
            }
        }

        private void RegisterUnits()
        {
             UnitFactory.Register((int)Defaults.UNITS.ARCHER, () => 
                new Archer(Defaults.Archer.price, Defaults.Archer.health, Defaults.Archer.attack, Defaults.Archer.defence, Defaults.Archer.specialActionStrength, Defaults.Archer.range));

            UnitFactory.Register((int)Defaults.UNITS.INFANTRY, () =>
                new Infantry(Defaults.Infantry.price, Defaults.Infantry.health, Defaults.Infantry.attack, Defaults.Infantry.defence, Defaults.Infantry.specialActionStrength, Defaults.Infantry.range));

            UnitFactory.Register((int)Defaults.UNITS.HEALER, () =>
                new Healer(Defaults.Healer.price, Defaults.Healer.health, Defaults.Healer.attack, Defaults.Healer.defence, Defaults.Healer.specialActionStrength, Defaults.Healer.range));

            UnitFactory.Register((int)Defaults.UNITS.KNIGHT, () =>
                new Knight(Defaults.Knight.price, Defaults.Knight.health, Defaults.Knight.attack, Defaults.Knight.defence));

            UnitFactory.Register((int)Defaults.UNITS.WIZARD, () =>
               new Wizard(Defaults.Wizard.price, Defaults.Wizard.health, Defaults.Wizard.attack, Defaults.Wizard.defence, Defaults.Wizard.specialActionStrength, Defaults.Wizard.range));

            UnitFactory.Register((int)Defaults.UNITS.WANDERING_TOWN, () => new WanderingTownAdapter(new WanderingTown()));
        }

        private static void RegisterFlags(int id)
        {
            _isMoneyEnoughDict.Add(id, true);
        }

        private static bool IsMoneyEnough()
        {
            bool result = false;

            foreach (var flag in _isMoneyEnoughDict)
            {
                result = result || flag.Value;
            }

            return result;
        }

        private static void RecalculateMoneyFlags(int money)
        {
            _isMoneyEnoughDict = _isMoneyEnoughDict.Select(v => {
                IUnit obj = _unitsDict[v.Key]();
                return new KeyValuePair<int, bool>(v.Key, obj.Cost <= money);
            }).ToDictionary(v => v.Key, v=> v.Value);
        }
    }
}
