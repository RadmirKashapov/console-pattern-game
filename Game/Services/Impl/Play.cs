using ConsoleGame.Army.Units;
using ConsoleGame.Army.Units.Impl;
using NetCoreAudio;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleGame.Game.Services.Impl
{
    class Play
    {
        public UserArmy FirstPlayerArmy { get; set; }
        public UserArmy SecondPlayerArmy { get; set; }
        private IMode GameMode { get; set; }
        public bool EndOfGame { get; set; }
        public string StepInfo { get; set; }
        public string GameInfo { get; set; }

        public Play(UserArmy firstArmy, UserArmy secondArmy, IMode mode)
        {
            FirstPlayerArmy = firstArmy;
            SecondPlayerArmy = secondArmy;
            GameMode = mode;
            EndOfGame = false;
        }

        public void SetGameMode(IMode mode)
        {
            this.GameMode = mode;
        }

        public void PlayToTheEnd()
        {
            while (!EndOfGame)
            {
                Step();
                GameInfo += StepInfo;
                Console.WriteLine(StepInfo);
            }
        }
        private List<IUnit> GetFirstLine(UserArmy army)
        {
            var firstLine = new List<IUnit>();
            var min = Math.Min(GameMode.rowSize, army.Count()); //независимо от вариации игры мы будем получать точное количество бойцов в ряду
            for (int i = 0; i < min; i++)
                firstLine.Add(army[army.Count() - 1 - i]); //в 1 на 1 первым к бою приступит боец под индексом 4(если армия из 5)
            return firstLine;
        }

        public void Fight(UserArmy first, UserArmy second)
        {
            if (GameOver()) return;


            List<IUnit> firstLineInFirst = GetFirstLine(first);
            List<IUnit> firstLineInSecond = GetFirstLine(second);


            //Сражаться могут только те бойцы, у которых есть оппонент=> Нужно четко определить "пары"
            var min = Math.Min(GameMode.rowSize, Math.Min(first.Count(), second.Count()));
            for (int i = 0; i < min; i++)
            {
                IUnit kicker = firstLineInFirst[i];
                IUnit defender = firstLineInSecond[i];

                StepInfo += $"\n\n {first.Name}. {kicker.GetInfo()}\n\n\t СРАЖАЕТСЯ С\n\n{second.Name}. {defender.GetInfo()}";

                IUnit dead = kicker.TakeDamage(defender);

                if (dead == null)
                {
                    StepInfo += $"\n\n{second.Name}. \n\n\t{defender.Name} ПОГИБ :(\n";

                    defender.DeathNotifier();

                    second.Remove(defender);
                }
                else
                    StepInfo += $"\n\n{second.Name}. \n\n\t АТАКУЕТ => {defender.GetInfo()}\n";
            }
        }

        private List<ISpecialAction> GetSpecialUnitsInRow(UserArmy army, int column)
        {
            var specials = new List<ISpecialAction>();
            int i = army.Count() - column - 1;
            while (i >= 0)
            {
                if (((Proxy)army[i]).Unit is ISpecialAction)
                {
                    specials.Add((ISpecialAction)(((Proxy)army[i]).Unit));

                }

                i -= GameMode.rowSize;
            }
            return specials;
        }

        private List<IUnit> GetTargets(UserArmy first, UserArmy second, ISpecialAction unit)
        {
            if (unit is Archer)
                return GameMode.GetArcherTargets(first, second, unit);
            else
            if (unit is Infantry)
            {
                return GameMode.GetInfantryTargets(first, unit);
            }
            else
            {
                return GameMode.GetDoctorTargets(first, unit);
            }
        }

        //private void DoSpecialAction(UserArmy one, UserArmy other)
        //{
        //    if (GameOver()) return;

        //    for (int i = 0; i < GameMode.rowSize; i++)
        //    {
        //        var specials = GetSpecialUnitsInRow(one, i);
        //        if (specials.Count == 0) continue;

        //        Random rnd = new Random();
        //        int indexSpecial = rnd.Next(0, specials.Count - 1);
        //        var victims = GetTargets(one, other, specials[indexSpecial]);



        //        if (victims.Count == 0) continue;

        //        int indexVictim = rnd.Next(0, victims.Count - 1);


        //        IUnit beforeSpecial = (IUnit)victims[indexVictim].Clone();
        //        IUnit afterSpecial = specials[indexSpecial].DoSpecialAction(victims[indexVictim]);

        //        StepInfo += "\nSpecial action.";

        //        if (specials[indexSpecial] is Archer)
        //        {
        //            StepInfo += $"\n\n{one.Name}. {((IUnit)specials[indexSpecial]).GetInfo()}\n\n\t СРАЖАЕТСЯ ПРОТИВ \n\n Армии {other.Name}. {beforeSpecial.GetInfo()}";

        //            if (afterSpecial == specials[indexSpecial])
        //            {
        //                StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nАрмия {other.Name}. {victims[indexVictim].Name} ПОГИБ :(\n";

        //                afterSpecial.DeathNotifier();

        //                other.Remove(afterSpecial);
        //            }
        //            else
        //                StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nАрмия {other.Name}. {victims[indexVictim].GetInfo()} РАНЕН \n";

        //        }

        //        else if (specials[indexSpecial] is Healer)
        //        {
        //            if (afterSpecial != null)
        //            {
        //                StepInfo += $"\n{one.Name}. {((IUnit)specials[indexSpecial]).GetInfo()}\n\n\t!!!!!! ЛЕЧИТ!!!!!!\n\nАрмия {one.Name}. {beforeSpecial.GetInfo()}";
        //                StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nАрмия {one.Name}. \n\t{victims[indexVictim].GetInfo()} ВЫЛЕЧЕН\n";
        //            }
        //            else
        //            {
        //                StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nНа этом шаге никого НЕ вылечили {one.Name}.";
        //            }
        //        }

        //        else if (specials[indexSpecial] is Wizard)
        //        {
        //            if (afterSpecial != null)
        //            {
        //                StepInfo += $"\nАрмия {one.Name}. {((IUnit)specials[indexSpecial]).GetInfo()}\n\n\tMAGIC TIME! Let's CLONE!\n\nАрмия {one.Name}. {beforeSpecial.GetInfo()}";
        //                StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nАрмия {one.Name}. {victims[indexVictim].GetInfo()} УСПЕШНО КЛОНИРОВАН.\n";
        //            }
        //            else
        //            {
        //                StepInfo += $"\n\t\t\t||\n\t\t\t\\/\n ДУХИ МОЛЧАТ - НИКТО НЕ КЛОНИРОВАН {one.Name}. ";
        //            }
        //        }

        //        else
        //        {
        //            if (specials[indexSpecial] is Infantry)
        //            {
        //                if (afterSpecial != null)
        //                {
        //                    StepInfo += $"\nАрмия {one.Name}. {((IUnit)specials[indexSpecial]).GetInfo()}\n\n\tОДЕВАЕТ\n\nArmy {one.Name}. {beforeSpecial.GetInfo()}";
        //                    StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nАрмия {one.Name}. {victims[indexVictim].GetInfo()} ОДЕТ В СПЕЦИАЛЬНУЮ ЭКИПИРОВКУ.\n";
        //                }
        //                else
        //                {
        //                    StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nНИКТО НЕ ОДЕТ В СПЕЦИАЛЬНУЮ ЭКИПИРОВКУ {one.Name}. ";
        //                }

        //            }

        //        }


        //    }
        //}

        private void DoSpecialAction(UserArmy one, UserArmy other)
        {
            if (GameOver()) return;

            for (int i = 0; i < GameMode.rowSize; i++)
            {
                var specials = GetSpecialUnitsInRow(one, i);
                if (specials.Count == 0) continue;

                Random rnd = new Random();
                int indexSpecial = rnd.Next(0, specials.Count - 1);
                var victims = GetTargets(one, other, specials[indexSpecial]);



                if (victims.Count == 0) continue;

                int indexVictim = rnd.Next(0, victims.Count - 1);


                IUnit beforeSpecial = (IUnit)victims[indexVictim].Clone();
                IUnit afterSpecial = specials[indexSpecial].DoSpecialAction(victims[indexVictim]);

                StepInfo += "\nSpecial action.";

                if (specials[indexSpecial] is Archer)
                {
                    StepInfo += $"\n\n{one.Name}. {((IUnit)specials[indexSpecial]).GetInfo()}\n\n\t СРАЖАЕТСЯ ПРОТИВ \n\n Армии {other.Name}. {beforeSpecial.GetInfo()}";

                    if (afterSpecial == specials[indexSpecial])
                    {
                        StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nАрмия {other.Name}. {victims[indexVictim].Name} ПОГИБ :(\n";

                        afterSpecial.DeathNotifier();

                        other.Remove(afterSpecial);
                    }
                    else
                        StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nАрмия {other.Name}. {victims[indexVictim].GetInfo()} РАНЕН \n";

                }

                else if (specials[indexSpecial] is Healer)
                {
                    if (afterSpecial != null)
                    {
                        StepInfo += $"\n{one.Name}. {((IUnit)specials[indexSpecial]).GetInfo()}\n\n\t!!!!!! ЛЕЧИТ!!!!!!\n\nАрмия {one.Name}. {beforeSpecial.GetInfo()}";
                        StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nАрмия {one.Name}. \n\t{victims[indexVictim].GetInfo()} ВЫЛЕЧЕН\n";
                    }
                    else
                    {
                        StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nНа этом шаге никого НЕ вылечили {one.Name}.";
                    }
                }

                else if (specials[indexSpecial] is Wizard)
                {
                    StepInfo += $"\nИндекс колдуна: {indexSpecial}";
                    StepInfo += $"\nИндекс жертвы: {indexVictim}";
                    if (afterSpecial != null)
                    {
                        StepInfo += $"\nАрмия {one.Name}. {((IUnit)specials[indexSpecial]).GetInfo()}\n\n\tMAGIC TIME! Let's CLONE!\n\nАрмия {one.Name}. {beforeSpecial.GetInfo()}";
                        StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nАрмия {one.Name}. {victims[indexVictim].GetInfo()} УСПЕШНО КЛОНИРОВАН.\n";
                        one.Push(afterSpecial);
                    }
                    else
                    {
                        StepInfo += $"\n\t\t\t||\n\t\t\t\\/\n ДУХИ МОЛЧАТ - НИКТО НЕ КЛОНИРОВАН {one.Name}. ";
                    }
                }

                else
                {
                    if (specials[indexSpecial] is Infantry)
                    {
                        if (afterSpecial != null)
                        {
                            StepInfo += $"\nАрмия {one.Name}. {((IUnit)specials[indexSpecial]).GetInfo()}\n\n\tОДЕВАЕТ\n\nArmy {one.Name}. {beforeSpecial.GetInfo()}";
                            StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nАрмия {one.Name}. {victims[indexVictim].GetInfo()} ОДЕТ В СПЕЦИАЛЬНУЮ ЭКИПИРОВКУ.\n";
                        }
                        else
                        {
                            StepInfo += $"\n\t\t\t||\n\t\t\t\\/\nНИКТО НЕ ОДЕТ В СПЕЦИАЛЬНУЮ ЭКИПИРОВКУ {one.Name}. ";
                        }

                    }

                }


            }
        }

        public void Step()
        {
            if (EndOfGame)
            {
                StepInfo = "Game over. ";
                return;
            }

            StepInfo = "\nВедется бой. ";

            Fight(FirstPlayerArmy, SecondPlayerArmy);
            Fight(SecondPlayerArmy, FirstPlayerArmy);
            DoSpecialAction(FirstPlayerArmy, SecondPlayerArmy);
            DoSpecialAction(SecondPlayerArmy, FirstPlayerArmy);
        }


        public bool GameOver()
        {
            if (EndOfGame)
                return true;

            if (FirstPlayerArmy.IsEmpty() || SecondPlayerArmy.IsEmpty())
            {
                EndOfGame = true;
                StepInfo += "\n\n Game over. ";
                if (FirstPlayerArmy.IsEmpty())
                    StepInfo += $"Победила армия {SecondPlayerArmy.Name}. \n";
                else
                    StepInfo += $"Победила армия {FirstPlayerArmy.Name}. \n";

                var player = new Player();
                player.Play(@"C:\Users\mylif\source\repos\Projects\ConsoleGame\win.wav");

                return true;
            }
            return false;
        }

        public string GetStepInfo()
        {
            return StepInfo;
        }

        public string GetGameInfo()
        {
            return GameInfo;
        }

    }
}
