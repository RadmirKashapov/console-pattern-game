using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Army.Units.Impl
{
    static class Defaults
    {

        public enum UNITS
        {
            INFANTRY, ARCHER, HEALER, KNIGHT, WIZARD
        }
        public static class Infantry
        {
            public static readonly int health = 100;
            public static readonly int price = 20;
            public static readonly int attack = 40;
            public static readonly int defence = 30;
            public static readonly int range = 1;
            public static readonly string name = "Пехотинец";
            public static readonly int specialActionStrength = 0;
        }

        public static class Knight
        {
            public static readonly int health = 100;
            public static readonly int price = 70;
            public static readonly int attack = 70;
            public static readonly int defence = 85;
            public static readonly string name = "Рыцарь";
        }

        public static class Archer
        {
            public static readonly int health = 100;
            public static readonly int price = 70;
            public static readonly int attack = 50;
            public static readonly int defence = 20;
            public static readonly string name = "Лучник";
            public static readonly int range = 5;
            public static readonly int specialActionStrength = 80;
        }

        public static class Healer
        {
            public static readonly int health = 100;
            public static readonly int price = 85;
            public static readonly int attack = 30;
            public static readonly int defence = 30;
            public static readonly string name = "Доктор";
            public static readonly int range = 1;
            public static readonly int specialActionStrength = 50;
        }

        public static class Wizard
        {
            public static readonly int health = 100;
            public static readonly int price = 90;
            public static readonly int attack = 40;
            public static readonly int defence = 40;
            public static readonly string name = "Колдун";
            public static readonly int range = 1;
            public static readonly int specialActionStrength = 50;
        }
    }
}
