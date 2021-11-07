using GameClasses;
using GameClasses.Enums;
using SteamClientLab.Enums;
using SteamClientLab.MenuPages;
using System;
using System.Threading;

namespace SteamClientLab.Model

{
    public class SteamClient
    {
        public delegate ReturnedData DelegateMethodConsolMenu(string titl, string action, params string[] titlAndMenuItems);
        public static DelegateMethodConsolMenu CallbackConsoleMenu { get; private set; }
        public Account CurrentAccaunt { get; set; } = null;

        private Account[] accounts = Array.Empty<Account>();

        public SteamClient(DelegateMethodConsolMenu externalMethodPrintMenu)
        {
            CallbackConsoleMenu = externalMethodPrintMenu;
        }

        /// <summary>
        /// Метод Запуска SteamClient
        /// </summary>
        /// <param name="externalMethod">ссылка на метод который способен взаимодействует с SteamClient</param>
        public void Start()
        {
            AdminAccountInitializer(ref accounts);

            while (CurrentAccaunt == null)
            {
                if (Autorization().ExecutionStatusCode == ExecutionStatusCode.ExitBeforeCompletion)
                {
                    return;
                }
            } 

            do
            {
                // дописать меню пользователя
               // steamClient.ActionIntoAccount();

            } while (CurrentAccaunt != null);
        }

        private ReturnedData Autorization()
        {
            bool isResponseValid = false;

            int selectedMenuItem;

            do
            {
                ReturnedData returnedData = CallbackConsoleMenu("Меню Авторизации", "Выберите пункт или Esc для выхода", MenuAutorizationText.menuItems);

                if (returnedData.ExecutionStatusCode==ExecutionStatusCode.ExitBeforeCompletion)
                {
                    return returnedData;
                }

                isResponseValid = int.TryParse(returnedData.ReturnedString, out selectedMenuItem)
                      && selectedMenuItem >= 0 && selectedMenuItem <= MenuAutorizationText.menuItems.Length;

            } while (!isResponseValid);

            switch (selectedMenuItem)
            {
                case 0:
                    return logining();

                case 1:
                    return AddNewAccaunt(ref accounts);

                case 2:
                    return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion };

                default:
                    return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.EnteredIncorrectValue };
            }
        }

        //private ReturnedData ActionIntoAccount()
        //{
        //    int selectedMenuItem;

        //    bool isResponseValid;

        //    ReturnedData returnedData = CallbackConsoleMenu($"Пользователь {CurrentAccaunt.NicName}", MenuUser.ActionForMenu, MenuUser.menuItems);

        //    isResponseValid = int.TryParse(returnedData.ReturnedString, out selectedMenuItem)
        //          && selectedMenuItem >= 0 && selectedMenuItem <= MenuUser.menuItems.Length;

        //    //switch (selectedMenuItem)
        //    //{
        //    //    case 0:
        //    //         PlayTheGame();
        //    //        break;

        //    //    case 1:
        //    //        ListGameForPurchase();
        //    //        break;

        //    //    case 2:
        //    //        BalanceManagement();
        //    //        break;

        //    //    case 3:
        //    //        ExitFromAccaunt();
        //    //        break;

        //    //    case 4:
        //    //        ExitFromSteam();
        //    //        break;

        //    //    default:
        //    //        break;
        //    //}

        //}

        //private void NewMethod1(Delegate @delegate,string titl ,string action , MenuUserEnum menuUserEnum )
        //{
        //    ReturnedData returnedData = CallbackConsoleMenu($"Пользователь {CurrentAccaunt.NicName}", MenuUser.ActionForMenu, MenuUser.menuItems);

        //    do
        //    {
        //        ReturnedData returnedData = CallbackConsoleMenu("Меню Авторизации", "Выберите пункт", MenuAutorizationText.menuItems);

        //        isResponseValid = int.TryParse(returnedData.ReturnedString, out selectedMenuItem);

        //        isResponseValid = int.TryParse(returnedData.ReturnedString, out selectedMenuItem)
        //              && selectedMenuItem >= 0 && selectedMenuItem <= MenuAutorizationText.menuItems.Length;

        //    } while (!isResponseValid);
        //}

        private ReturnedData logining()
        {
            string login;
            string password;

            ReturnedData returnedDataLogin = CallbackConsoleMenu("Вход в Аккаунт", "Введите логин");
            login = returnedDataLogin.ExecutionStatusCode == ExecutionStatusCode.CorrectCompletion ? returnedDataLogin.ReturnedString : string.Empty;

            ReturnedData returnedDataPassword = CallbackConsoleMenu("Вход в Аккаунт", "Введите пароль");
            password = returnedDataPassword.ExecutionStatusCode == ExecutionStatusCode.CorrectCompletion ? returnedDataPassword.ReturnedString : string.Empty;

            Account TempAccaunt = FindAccountToLigin(login, accounts);

            if (TempAccaunt != null && TempAccaunt.Password == password)
            {
                CurrentAccaunt = TempAccaunt;
                CurrentAccaunt.IsAuthorized = true;

                Console.WriteLine($"Вы вошли в аакант:");
                Console.WriteLine($"{CurrentAccaunt.GetAccauntData()}");
                Thread.Sleep(6000);

                return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion };
            }

            Console.WriteLine($"Логин и/или пароль не верны");
            Thread.Sleep(1000);

            return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.EnteredIncorrectValue };

        }

        private Account FindAccountToLigin(string login, Account[] accounts)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if ((accounts[i].Login) == login)
                {
                    return accounts[i];
                }
            }
            return null;
        }

        private ReturnedData AddNewAccaunt(ref Account[] accounts)
        {

            Account tempAccaunt = RegistrationNewAccaunt();

            if (tempAccaunt == null)
            {
                return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion };
            }

            Account[] tempAccaunts = new Account[accounts.Length + 1];

            for (int i = 0; i < tempAccaunts.Length; i++)
            {
                if (tempAccaunts.Length - 1 == i)
                {
                    tempAccaunts[i] = tempAccaunt;
                }
            }
            accounts = tempAccaunts;
            return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion };
        }

        private ReturnedData AddNewAccaunt(Account account, ref Account[] accounts)
        {

            Account tempAccaunt = account;

            if (tempAccaunt == null)
            {
                return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion };
            }

            Account[] tempAccaunts = new Account[accounts.Length + 1];

            for (int i = 0; i < tempAccaunts.Length; i++)
            {
                //tempAccaunts[i] = accounts.Length - 1 == i ? tempAccaunt : accounts[i];
                if (tempAccaunts.Length - 1 == i)
                {
                    tempAccaunts[i] = tempAccaunt;
                }

            }
            accounts = tempAccaunts;
            return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion };
        }

        private Account RegistrationNewAccaunt()
        {
            string fio = string.Empty;
            Sex sex = Sex.Man;
            string nicName = string.Empty;
            int age = 0;
            decimal balance = 0;
            string login = string.Empty;
            string password = string.Empty;

            bool isResponseValid = false;

            do
            {
                ReturnedData tempReturnedData = CallbackConsoleMenu("Регистрация Аккаунта", "ФИО через пробел");

                if (tempReturnedData.ExecutionStatusCode == ExecutionStatusCode.ExitBeforeCompletion)
                {
                    return null;
                }
                else if (tempReturnedData.ExecutionStatusCode == ExecutionStatusCode.CorrectCompletion)
                {
                    fio = tempReturnedData.ReturnedString;
                }

                isResponseValid = fio.Split(" ").Length >= 2 && fio.Split(" ").Length <= 3;

            } while (!isResponseValid);


            int selectedMenuItem;

            do
            {
                isResponseValid = int.TryParse(CallbackConsoleMenu("Регистрация Аккаунта", "Выберите пол", MenuSexSelect.menuItems).ReturnedString, out selectedMenuItem)
                && selectedMenuItem >= 0 && selectedMenuItem <= 1;

            } while (!isResponseValid);

            sex = (Sex)selectedMenuItem;

            nicName = CallbackConsoleMenu("Регистрация Аккаунта", "Введите Ник для игры").ReturnedString;

            do
            {
                isResponseValid = int.TryParse(CallbackConsoleMenu("Регистрация Аккаунта", "Введите возраст").ReturnedString, out age) && age >= 0 && age <= 130;

            } while (!isResponseValid);

            do
            {
                isResponseValid = decimal.TryParse(CallbackConsoleMenu("Регистрация Аккаунта", "Введите начальный баланс").ReturnedString, out balance) && balance >= 0;

            } while (!isResponseValid);

            login = CallbackConsoleMenu("Регистрация Аккаунта", "Введите логин").ReturnedString;

            password = CallbackConsoleMenu("Регистрация Аккаунта", "Введите пароль").ReturnedString;

            Account tempAccaunt = new Account(fio, sex, nicName, age, balance, login, password);

            if (tempAccaunt != null)
            {
                Console.WriteLine("Аккаунт создан");
                Console.WriteLine($"{tempAccaunt.GetAccauntData()}");

                Thread.Sleep(2000);
            }

            return tempAccaunt;
        }

        public ReturnedData AdminAccountInitializer(ref Account[] accounts)
        {
            Account tempAccaunt = new Account("Админ Админович Админовский", Sex.Man, "Admin", 100, 1000000, "admin", "1234");

            AddNewAccaunt(tempAccaunt, ref accounts);

            return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion };
        }

    }
}
