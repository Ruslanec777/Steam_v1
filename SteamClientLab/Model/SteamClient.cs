using GameClasses;
using GameClasses.Enums;
using SteamClientLab.Enums;
using SteamClientLab.MenuPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace SteamClientLab.Model
{
    public class SteamClient
    {
        public delegate string DelegateMethodConsolMenu(string titl, string action, params string[] titlAndMenuItems);
        public static DelegateMethodConsolMenu CallbackConsoleMenu { get; private set; }
        public Account CurrentAccaunt { get; set; } 

        // private Account[] accounts = Array.Empty<Account>(); 
        List<Account> accounts = new List<Account>();

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

            while (true)
            {
                while (CurrentAccaunt == null)
                {
                    try
                    {
                        Autorization();
                    }
                    catch (MenuException exception)
                    {
                        if (exception.ErrorCode == MenuExceptions.ExitRequest)
                        {
                            return;
                        }
                        else if (exception.ErrorCode == MenuExceptions.ReturningBack)
                        {
                            continue;
                        }
                    }
                }

                do
                {
                    // дописать меню пользователя
                    // steamClient.ActionIntoAccount();

                } while (CurrentAccaunt != null);
            }
        }

        private void Autorization()
        {
            bool isResponseValid = false;
            int selectedMenuItem;

            do
            {
                try
                {
                    string responsMenu = CallbackConsoleMenu("Меню Авторизации", "Выберите пункт или Esc для выхода", MenuAutorizationText.menuItems);

                    isResponseValid = int.TryParse(responsMenu, out selectedMenuItem)
                          && selectedMenuItem >= 0 && selectedMenuItem <= MenuAutorizationText.menuItems.Length;
                }
                catch (Exception)
                {

                    throw new MenuException(MenuExceptions.ExitRequest);
                }

            } while (!isResponseValid);

            try
            {
                switch (selectedMenuItem)
                {
                    case 0:
                        logining();

                        return;

                    case 1:
                        AddNewAccaunt();

                        return;

                    default:
                        throw new MenuException(MenuExceptions.ReturningBack); 
                }
            }
            catch (Exception)
            {
                throw new MenuException(MenuExceptions.ReturningBack);
            }
        }
        /// <summary>
        /// Вернет ReturnedData с заменой ExecutionStatusCode на RequestToReturnThisMenu
        /// </summary>
        /// <param name="returnedDataForChecking"></param>
        /// <returns></returns>
        private static ReturnedData CheckingReturnMenuBelow(ReturnedData returnedDataForChecking)
        {
            if (returnedDataForChecking.ExecutionStatusCode == ExecutionStatusCode.ExitBeforeCompletion)
            {
                returnedDataForChecking.ExecutionStatusCode = ExecutionStatusCode.RequestToRestartThisMenu;
            }
            return returnedDataForChecking;
        }

        private Account logining()
        {
            string login;
            string password;

            login = CallbackConsoleMenu("Вход в Аккаунт", "Введите логин");
            password = CallbackConsoleMenu("Вход в Аккаунт", "Введите пароль");

            Account TempAccaunt = FindAccountToLigin(login);

            if (TempAccaunt != null && TempAccaunt.Password == password)
            {
                CurrentAccaunt = TempAccaunt;
                CurrentAccaunt.IsAuthorized = true;

                CallbackConsoleMenu("Зарегестрирован пользавотель:", TempAccaunt.GetAccauntData());

                return TempAccaunt;
            }

            return null;
        }

        private Account FindAccountToLigin(string login)
        {
            Account account = accounts.FirstOrDefault(i => i.Login == login);
            if (account == null)
            {
                return null;
            }
            return account;
        }

        private Account AddNewAccaunt()
        {

            Account tempAccaunt = RegistrationNewAccaunt();

            if (tempAccaunt!=null)
            {
                accounts.Add(tempAccaunt);
                return tempAccaunt;
            }

            return null;

        }

        private ReturnedData AddNewAccaunt(Account account, ref List<Account> accounts)
        {

            if (account == null)
            {
                return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion };
            }

            accounts.Add(account);

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
                fio = CallbackConsoleMenu("Регистрация Аккаунта", "ФИО через пробел");

                isResponseValid = fio.Split(" ").Length >= 2 && fio.Split(" ").Length <= 3;

            } while (!isResponseValid);


            int selectedMenuItem;

            do
            {
                string responsString = CallbackConsoleMenu("Регистрация Аккаунта", "Выберите пол", MenuSexSelect.menuItems);

                isResponseValid = int.TryParse(responsString, out selectedMenuItem)
                && selectedMenuItem >= 0 && selectedMenuItem <= 1;

            } while (!isResponseValid);
            sex = (Sex)selectedMenuItem;

            nicName = CallbackConsoleMenu("Регистрация Аккаунта", "Введите Ник для игры");

            do
            {

                isResponseValid = int.TryParse(CallbackConsoleMenu("Регистрация Аккаунта", "Введите возраст"), out age) && age >= 0 && age <= 130;

            } while (!isResponseValid);


            login = CallbackConsoleMenu("Регистрация Аккаунта", "Введите логин");

            password = CallbackConsoleMenu("Регистрация Аккаунта", "Введите пароль");

            CurrentAccaunt = new Account(fio, sex, nicName, age, balance, login, password); // заносим вновь созданный аккаунт в текущий , переделать( несколько действий в методе)

            return CurrentAccaunt;
        }

        public ReturnedData AdminAccountInitializer(ref List<Account> accounts)
        {
            Account tempAccaunt = new Account("Админ Админович Админовский", Sex.Man, "Admin", 100, 1000000, "admin", "1234");

            AddNewAccaunt(tempAccaunt, ref accounts);

            return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion };
        }

    }
}
