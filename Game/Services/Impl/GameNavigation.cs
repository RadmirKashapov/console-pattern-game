using ConsoleGame.Army.Units;
using ConsoleGame.Army.Units.Impl;
using ConsoleGame.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Text;

namespace ConsoleGame.Game.Services.Impl
{
    class GameNavigation
    {
        UserArmy firstArmy = new UserArmy();
        UserArmy secondArmy = new UserArmy();
        Logger logger = new Logger();
        CommandManager commandManager;
        IMode mode;
        private void Menu()
        {

            logger.Log("Класс навигации");

            Console.WriteLine();

            Console.WriteLine("1. Создать армию");
            Console.WriteLine("2. Показать состав армии");
            Console.WriteLine("3. Сделать ход");
            Console.WriteLine("4. Отменить ход");
            Console.WriteLine("5. Вернуть ход");
            Console.WriteLine("6. Играть до конца");
            Console.WriteLine("0. Выход");


            Console.WriteLine();

            string answer = Console.ReadLine();


            switch (answer)
            {
                case "1":

                    firstArmy.SetName("Армия джедаев");

                    secondArmy.SetName("Армия ситхов");

                    Console.WriteLine("Введите сумму, на которую необходимо заполнить 1-ую армию:");
                    int firstPlayerBank = Convert.ToInt32(Console.ReadLine());

                    firstArmy.SetBank(firstPlayerBank);
                    firstArmy.CreateArmy();

                    if (firstArmy.IsEmpty())
                    {
                        logger.Log("Ошибка создания армии");
                        Console.WriteLine("Невозможно создать армию");
                        Menu();
                        break;
                    }

                    logger.Log("Армия 1 создана");

                    Console.WriteLine("Введите сумму, на которую необходимо заполнить 2-ую армию:");
                    int secondPlayerBank = Convert.ToInt32(Console.ReadLine());

                    secondArmy.SetBank(secondPlayerBank);
                    secondArmy.CreateArmy();

                    if (secondArmy.IsEmpty())
                    {
                        logger.Log("Ошибка создания армии");
                        Console.WriteLine("Невозможно создать армию");
                        Menu();
                        break;
                    }


                    logger.Log("Армия 2 создана");

                    Console.WriteLine("Армии созданы");
                    logger.Log("Армии созданы");

                    Menu();
                    break;

                case "2":
                    Console.WriteLine("Армия 1");

                    if (firstArmy.IsEmpty() || secondArmy.IsEmpty())
                    {
                        Console.WriteLine("Армии не созданы");
                        Menu();
                        break;
                    }

                    Console.WriteLine(firstArmy.GetInfo());

                    Console.WriteLine(secondArmy.GetInfo());

                    Menu();
                    break;

                case "3":

                    if (firstArmy.IsEmpty())
                    {
                        Console.WriteLine("Армии не созданы");
                        Menu();
                        break;
                    }

                    ChooseGameModeMenu();

                    commandManager.Step();
                    var stepInfo = commandManager.GetStepInfo();
                    Console.WriteLine(stepInfo);
                    logger.Log(stepInfo);
                    Menu();
                    break;

                case "4":

                    if (firstArmy.IsEmpty())
                    {
                        Console.WriteLine("Армии не созданы");
                        Menu();
                        break;
                    }

                    commandManager.Undo();

                    Console.WriteLine("Ход отменен");
                    logger.Log("Ход отменен");


                    Menu();
                    break;

                case "5":

                    if (firstArmy.IsEmpty())
                    {
                        Console.WriteLine("Армии не созданы");
                        Menu();
                        break;
                    }

                    Console.WriteLine("Ход восстановлен");

                    commandManager.Redo();

                    logger.Log("Ход восстановлен");


                    Menu();
                    break;

                case "6":

                    if (firstArmy.IsEmpty())
                    {
                        Console.WriteLine("Армии не созданы");
                        Menu();
                        break;
                    }

                    ChooseGameModeMenu();

                    commandManager.PlayToTheEnd();

                    var info = commandManager.GetGameInfo();

                    logger.Log(info);

                    Menu();
                    break;
                case "0":
                    return;

                default:
                    Console.WriteLine("Ошибка. Выбранной операции нет в списке. Попробуйте еще раз.");

                    Menu();
                    break;

            }
        }

        public void ChooseGameModeMenu()
        {
            Console.WriteLine();

            Console.WriteLine("Выберите режим игры:");
            Console.WriteLine("1. 1 на 1");
            Console.WriteLine("2. 3 на 3");
            Console.WriteLine("3. Стенка на стенку");
            Console.WriteLine();

            string answer = Console.ReadLine();

            switch (answer)
            {
                case "1":

                    mode = new OneToOneGameMode();
                    break;

                case "2":

                    mode = new ThreeToThreeGameMode();
                    break;

                case "3":

                    mode = new NToMGameMode(Math.Min(firstArmy.Count(), secondArmy.Count()));
                    break;

                default:
                    Console.WriteLine("Ошибка. Выбранной операции нет в списке. Попробуйте еще раз.");

                    ChooseGameModeMenu();
                    break;
            }

            if (commandManager == null)
                commandManager = new CommandManager(firstArmy, secondArmy, mode);
            else
                commandManager.SetGameMode(mode);

        }

        public void Start()
        {
            Menu();
        }
    }
}
