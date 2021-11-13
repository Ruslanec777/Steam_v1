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
        public Account CurrentAccaunt { get; set; } = null;

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

            while (CurrentAccaunt == null)
            {
                try
                {
                    Autorization();
                }
                catch (MenuException exception)
                {
                    if (exception.ErrorCode==MenuExceptions.ReturningBack)
                    {
                        Console.WriteLine($"Выход из приложения ");
                        Thread.Sleep(1000);

                        return;
                    }               
                }
                catch (MenuExceptions)
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

                if (returnedData.ExecutionStatusCode == ExecutionStatusCode.ExitBeforeCompletion)
                {
                    return returnedData;
                }

                isResponseValid = int.TryParse(returnedData.ReturnedString, out selectedMenuItem)
                      && selectedMenuItem >= 0 && selectedMenuItem <= MenuAutorizationText.menuItems.Length;

            } while (!isResponseValid);

            switch (selectedMenuItem)
            {
                case 0:
                    return CheckingReturnMenuBelow(logining());

                case 1:
                    return CheckingReturnMenuBelow(AddNewAccaunt(ref accounts));

                default:
                    return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion };
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




        private ReturnedData logining()
        {
            string login;
            string password;

            ReturnedData returnedDataLogin = CallbackConsoleMenu("Вход в Аккаунт", "Введите логин");
            if (returnedDataLogin.ExecutionStatusCode == ExecutionStatusCode.ExitBeforeCompletion)
            {
                return returnedDataLogin;
            }
            login = returnedDataLogin.ReturnedString;

            ReturnedData returnedDataPassword = CallbackConsoleMenu("Вход в Аккаунт", "Введите пароль");
            if (returnedDataPassword.ExecutionStatusCode == ExecutionStatusCode.ExitBeforeCompletion)
            {
                return returnedDataPassword;
            }
            password = returnedDataPassword.ReturnedString;

            Account TempAccaunt = FindAccountToLigin(login, accounts);

            if (TempAccaunt != null && TempAccaunt.Password == password)
            {
                CurrentAccaunt = TempAccaunt;
                CurrentAccaunt.IsAuthorized = true;

                return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion };
            }

            return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.EnteredIncorrectValue };

        }

        private Account FindAccountToLigin(string login, List<Account> accounts)
        {
            Account account = accounts.FirstOrDefault(i => i.Login == login);
            if (account == null)
            {
                return null;
            }
            return account;
        }

        private ReturnedData AddNewAccaunt(ref List<Account> accounts)
        {

            Account tempAccaunt = null;

            ReturnedData returnedData = RegistrationNewAccaunt(ref tempAccaunt);

            if (!returnedData.Issuccessfull)
            {
                return CheckingReturnMenuBelow(returnedData);
            }

            accounts.Add(CurrentAccaunt); // берем вновь созданный аккаунт из текущено и помещаем в лист , переделать: принимать  аккаунт тут 

            return returnedData;
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

        private ReturnedData RegistrationNewAccaunt(ref Account account)
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

                if (!tempReturnedData.Issuccessfull)
                {
                    return CheckingReturnMenuBelow(tempReturnedData);
                }
                else if (tempReturnedData.Issuccessfull)
                {
                    fio = tempReturnedData.ReturnedString;
                }

                isResponseValid = fio.Split(" ").Length >= 2 && fio.Split(" ").Length <= 3;

            } while (!isResponseValid);


            int selectedMenuItem;

            do
            {
                ReturnedData tempReturnedData = CallbackConsoleMenu("Регистрация Аккаунта", "Выберите пол", MenuSexSelect.menuItems);
                if (!tempReturnedData.Issuccessfull)
                {
                    return CheckingReturnMenuBelow(tempReturnedData);
                }

                isResponseValid = int.TryParse(tempReturnedData.ReturnedString, out selectedMenuItem)
                && selectedMenuItem >= 0 && selectedMenuItem <= 1;

            } while (!isResponseValid);

            sex = (Sex)selectedMenuItem;

            ReturnedData returnedData = CallbackConsoleMenu("Регистрация Аккаунта", "Введите Ник для игры");

            if (!returnedData.Issuccessfull)
            {
                return CheckingReturnMenuBelow(returnedData);
            }
            else if (returnedData.Issuccessfull)
            {
                nicName = returnedData.ReturnedString;
            }


            do
            {

                isResponseValid = int.TryParse(CallbackConsoleMenu("Регистрация Аккаунта", "Введите возраст").ReturnedString, out age) && age >= 0 && age <= 130;

            } while (!isResponseValid);


            login = CallbackConsoleMenu("Регистрация Аккаунта", "Введите логин").ReturnedString;

            password = CallbackConsoleMenu("Регистрация Аккаунта", "Введите пароль").ReturnedString;

            CurrentAccaunt = new Account(fio, sex, nicName, age, balance, login, password); // заносим вновь созданный аккаунт в текущий , переделать( несколько действий в методе)

            return new ReturnedData();
        }

        public ReturnedData AdminAccountInitializer(ref List<Account> accounts)
        {
            Account tempAccaunt = new Account("Админ Админович Админовский", Sex.Man, "Admin", 100, 1000000, "admin", "1234");

            AddNewAccaunt(tempAccaunt, ref accounts);

            return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion };
        }

    }
}
