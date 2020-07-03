using ConsoleGame.Army.Units;
using ConsoleGame.Army.Units.Impl;
using ConsoleGame.Game.Services.Impl;
using ConsoleGame.Infrastructure;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace ConsoleGame
{
    class Program
    {

        static void Main(string[] args)
        {
            Logger logger = new Logger();

            Console.OutputEncoding = System.Text.Encoding.Unicode;

            string shrekPath = @"shrek.txt";
            string[] shrekArray = File.ReadAllLines(shrekPath);

            Console.WriteLine();

            foreach(var shrekLine in shrekArray)
            {
                Console.WriteLine(shrekLine);
            }

            logger.Log("Запуск программы");
            var gameNavigation = new GameNavigation();
            gameNavigation.Start();


            string catPath = @"cat.txt";
            string[] catArray = File.ReadAllLines(catPath);

            Console.WriteLine();

            foreach (var catLine in catArray)
            {
                Console.WriteLine(catLine);
            }

            logger.Log("Конец работы программы");
        }
    }
}
