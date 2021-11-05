﻿using Application.Enums;
using GameClasses;
using GameClasses.Enums;
using System;
using System.Threading;
using static System.Console;
using SteamClientLab.Model;
using SteamClientLab.Enums;

namespace ClassesProject
{
    class Program
    {

        //static Menu selectedMenuItem; // koment

        //static string tempStr = string.Empty;

        //static decimal tempDecimal;

        //static int newProp;

        //static int tempInt;

        //static Game[] allGames = new Game[0];

        //static Player player = null;

        static void Main(string[] args)
        {



            //WorkWithhMenu();

            //SteamClientLab.Model.SteamClient()
            SteamClient.Start(AdapterForSteamClient);

            return;
        }

        public static ReturnedData AdapterForSteamClient(string title, string action, params string[] menuItems)
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine(action);

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{i}. {menuItems[i]}");
            }


            //if (Console.ReadKey().Key == ConsoleKey.Escape)
            //{
            //    return new ReturnedData () {ExecutionStatusCode=ExecutionStatusCode.ExitBeforeCompletion};
            //}
            string inputString = Console.ReadLine();

            ReturnedData returnedData=new ReturnedData() { ExecutionStatusCode}

            return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion, ReturnedString = Console.ReadLine() };
        }

        public static ReturnedData AdapterForSteamClient(string title, string action)
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine(action);

            if (Console.ReadKey().Key != ConsoleKey.Escape)
            {
                return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion };
            }

            return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion, ReturnedString = Console.ReadLine() };
        }

        //private static void Autorization()
        //{
        //    int selectedItem = SelectMenuItem(2, "Есть ли у вас аккаунт ?\n" +
        //                                         "1.ДА        2.НЕТ");

        //}

        //private static int SelectMenuItem(int numberOfMenuItems, string menuText)
        //{
        //    int menuItem = 0;
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine(menuText);

        //        int.TryParse(ReadLine(), out menuItem);
        //        if (menuItem < 1 || menuItem > numberOfMenuItems)
        //        {
        //            Console.WriteLine("Неправильный ввод, введите цифру 1 или 2.");
        //            Thread.Sleep(1000);
        //        }

        //    } while (menuItem < 1 || menuItem > numberOfMenuItems);

        //    return menuItem;
        //}

        //private static void WorkWithhMenu()
        //{
        //    while (true)
        //    {
        //        PrintMenuList();

        //        switch (selectedMenuItem)
        //        {
        //            case Menu.PlayerCreation:
        //                player = CreationPlayer();
        //                break;

        //            case Menu.DisplayPlayerInformation:
        //                if (player != null) Console.WriteLine(player.GetFullName() + "\n" + player.GetPlayerData());
        //                else Console.WriteLine("Игроков не создано");
        //                Console.ReadLine();
        //                break;

        //            case Menu.AccountCreation:
        //                if (player != null) CreateAccaunt(player);
        //                else Console.WriteLine("Игрок не создан, для создания аккаунта , создайте игрока");
        //                Console.ReadLine();
        //                break;

        //            case Menu.AccountAuthorization:
        //                // не понятно что делать
        //                break;

        //            case Menu.RemoveAccount:
        //                if (player != null && player.Account != null)
        //                {
        //                    player.RemoveAccaunt();
        //                    Console.WriteLine("Аккаунт удален");
        //                }
        //                else Console.WriteLine("аккаунта не существует");
        //                Console.ReadLine();
        //                break;

        //            case Menu.AddMoneyToAccaunt:

        //                if (player != null && player.Account != null)
        //                {
        //                    do
        //                    {
        //                        Console.Clear();
        //                        Console.WriteLine("Введите сумму для зачисления на аккаунт с баланса игрока");
        //                        tempStr = Console.ReadLine();

        //                    } while (!Decimal.TryParse(tempStr, out tempDecimal));

        //                    player.AddManeyToAccaunt(player.Account, tempDecimal);
        //                    Console.WriteLine($"Баланс аккаунта пополнен и составляет{player.Account.Balance} , баланс игрока составляет {player.Balance}");
        //                }
        //                Console.ReadLine();

        //                break;

        //            case Menu.DisplayAccauntInformation:

        //                if (player != null && player.Account != null)
        //                {
        //                    Console.WriteLine(player.Account.GetAccauntData());
        //                }
        //                else Console.WriteLine("аккаунта не существует");
        //                Console.ReadLine();

        //                break;

        //            case Menu.RemoveMoneyFromAccount:

        //                if (player != null && player.Account != null)
        //                {
        //                    do
        //                    {
        //                        Console.Clear();
        //                        Console.WriteLine("Введите сумму для списания с аккаунта на баланс игрока");
        //                        tempStr = Console.ReadLine();

        //                    } while (!Decimal.TryParse(tempStr, out tempDecimal));
        //                    player.RemoveMoneyFromAccount(player.Account, tempDecimal);
        //                    Console.WriteLine($"Баланс игрока пополнен и составляет{player.Balance} , баланс аккаунта составляет {player.Account.Balance}");
        //                }
        //                Console.ReadLine();

        //                break;

        //            case Menu.CreateGame:

        //                CreateNewGame(allGames);
        //                ReadLine();

        //                break;

        //            case Menu.BuyGame:

        //                Console.Clear();
        //                Console.WriteLine(selectedMenuItem);
        //                tempStr = ReadLine();

        //                BuyGame(tempStr, allGames);

        //                break;

        //            case Menu.PlayGame:

        //                Console.Clear();
        //                Console.WriteLine(selectedMenuItem);
        //                WriteLine("В какую игру хотите играть ?");
        //                tempStr = ReadLine();
        //                FindGameForName(tempStr, player.Account.Games);

        //                break;

        //            case Menu.QuitTheGame:
        //                break;

        //            case Menu.Exit:
        //                Console.WriteLine("Выход из программы");
        //                return;
        //                break;

        //            default:
        //                break;
        //        }
        //    }
        //}

        //private static void PrintMenuList()
        //{
        //    do
        //    {
        //        Console.Clear();

        //        for (int i = 0; i < Enum.GetValues<Menu>().Length; i++)
        //        {
        //            Console.WriteLine($"{i}. -{(Menu)i}");
        //        }

        //        tempStr = Console.ReadLine();

        //    } while (!(Enum.TryParse<Menu>(tempStr, out selectedMenuItem) && Enum.IsDefined(typeof(Menu), selectedMenuItem)));
        //}

        //private static void CreateNewGame(Game[] games)
        //{
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine(selectedMenuItem);
        //        WriteLine("Введите название новой игры");
        //        tempStr = ReadLine();

        //        WriteLine("Введите стоимость новой игры");

        //    } while (!int.TryParse(ReadLine(), out tempInt));

        //    Game[] tempArray = new Game[games.Length + 1];

        //    for (int i = 0; i < games.Length + 1; i++)
        //    {
        //        if (i == games.Length)
        //        {
        //            tempArray[i] = new Game(tempStr, tempInt);
        //        }
        //        tempArray[i] = games[i];
        //    }
        //    games = tempArray;

        //    WriteLine($"Новая игра создана ");
        //}

        //private static void BuyGame(string name, Game[] games)
        //{
        //    WriteLine(FindGameForName(name, games).SaleGame(player.Account) ? "Игра куплена" : "Игра не куплена");

        //}

        //private static Game FindGameForName(string name, Game[] games)
        //{
        //    for (int i = 0; i < games.Length; i++)
        //    {
        //        if (games[i].Name == name)
        //        {
        //            return games[i];
        //        }
        //    }

        //    return null;
        //}

        //private static void CreateAccaunt(Player player)
        //{
        //    string tempLogin;
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine(selectedMenuItem);
        //        Console.WriteLine("Введите Логин не менее 6 символов");
        //        tempLogin = Console.ReadLine();
        //    } while (tempLogin.Length < 6);

        //    string tempPassword;
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine(selectedMenuItem);
        //        Console.WriteLine("Введите Пароль не менее 6 символов");
        //        tempPassword = Console.ReadLine();
        //    } while (tempPassword.Length < 6);

        //    if (player != null)
        //    {
        //        player.CreateAccount(tempLogin, tempPassword);

        //    }
        //    Console.WriteLine("Аккаунт создан ");
        //}

        //static Player CreationPlayer()
        //{
        //    Console.Clear();
        //    Console.WriteLine(selectedMenuItem);
        //    Console.WriteLine("Ник:");
        //    string nic = Console.ReadLine();

        //    string[] temStrings;
        //    string fio;
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine("ФИО , вводится через пробел (Петров Петр Петрович):");
        //        fio = Console.ReadLine();
        //        temStrings = fio.Split(" ", StringSplitOptions.RemoveEmptyEntries);
        //    } while (temStrings.Length < 2 || temStrings.Length > 3);

        //    int age = 0;
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Возраст:");
        //    } while (!int.TryParse(Console.ReadLine(), out age) || age < 0 || age > 130);

        //    Sex sex = Sex.Man;
        //    string temString = string.Empty;
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Пол (Man/Woman):");
        //        temString = Console.ReadLine();

        //    } while (!Enum.TryParse<Sex>(temString, out sex) || !Enum.IsDefined(typeof(Sex), sex));


        //    decimal maney = 0;
        //    do
        //    {
        //        Console.Clear();
        //        Console.WriteLine("Деньги игрока:");
        //    } while (!(decimal.TryParse(Console.ReadLine(), out maney) || maney < 0));

        //    return new Player(nic, fio, age, sex, maney);
        //}



        //static string ReadLineAndCheckExit()
        //{
        //    string tempStr = Console.ReadLine();

        //    if (tempStr.ToLower().Trim() == "exit")
        //    {
        //        return "exit";
        //    }
        //    return tempStr;
        //}

        //static void CreatePlayer()
        //{
        //    while (true)
        //    {
        //        Console.WriteLine("Cоздайте игрока (enter)" + exitOffer);

        //        if (ReadLineAndCheckExit() == "exit")
        //        {
        //            Console.Clear();
        //            break;
        //        }
        //        player = CreationPlayer();
        //        if (player == null)
        //        {
        //            Console.Clear();
        //            continue;
        //        } 
        //    }

        //    Console.Clear();
        //    Console.WriteLine("Игрок успешно Создан ");
        //    Console.WriteLine(player.GetFullName());
        //    Console.WriteLine(player.GetPlayerData());

        //    Console.ReadLine();
        //    Console.Clear();
        //}
    }
}