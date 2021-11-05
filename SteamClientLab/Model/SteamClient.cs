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

        private SteamClient(DelegateMethodConsolMenu externalMethodPrintMenu)
        {
            CallbackConsoleMenu = externalMethodPrintMenu;
        }

        /// <summary>
        /// Метод Запуска SteamClient
        /// </summary>
        /// <param name="externalMethod">ссылка на метод который способен взаимодействует с SteamClient</param>
        public static void Start(DelegateMethodConsolMenu externalMethod)
        {
            SteamClient steamClient = new SteamClient(externalMethod);

            do
            {
                if (steamClient.Autorization().ExecutionStatusCode == ExecutionStatusCode.ExitBeforeCompletion)
                {
                    return;
                }

            } while (steamClient.CurrentAccaunt == null);


        }

        private ReturnedData Autorization()
        {
            bool isResponseValid = false;

            int selectedMenuItem;

            do
            {
                ReturnedData returnedData = CallbackConsoleMenu("Меню Авторизации", "Выберите пункт", MenuAutorizationText.menuItems);

                isResponseValid = int.TryParse(returnedData.ReturnedString, out selectedMenuItem);

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

        private ReturnedData logining()
        {
            string login;
            string password;

            ReturnedData returnedDataLogin = CallbackConsoleMenu("Вход в Аккаунт", "Введите логин");

            if (returnedDataLogin.ExecutionStatusCode == ExecutionStatusCode.ExitBeforeCompletion)
            {
                return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion };
            }

            login = returnedDataLogin.ExecutionStatusCode == ExecutionStatusCode.CorrectCompletion ? returnedDataLogin.ReturnedString : string.Empty;

            ReturnedData returnedDataPassword = CallbackConsoleMenu("Вход в Аккаунт", "Введите пароль");

            if (returnedDataLogin.ExecutionStatusCode == ExecutionStatusCode.ExitBeforeCompletion)
            {
                return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion };
            }

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

            if (tempAccaunt!=null)
            {
                Console.WriteLine("Аккаунт создан");
                Console.WriteLine($"{tempAccaunt.GetAccauntData()}");

                Thread.Sleep(2000);
            }

            return tempAccaunt ;
        }

    }
}
