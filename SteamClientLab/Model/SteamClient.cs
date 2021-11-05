﻿using GameClasses;
using GameClasses.Enums;
using SteamClientLab.Enums;
using SteamClientLab.MenuPages;
using System;

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
                if (steamClient.Autorization().ExecutionStatusCode==ExecutionStatusCode.ExitBeforeCompletion)
                {
                    return;
                } 

            } while (steamClient.CurrentAccaunt==null);

            steamClient.Autorization();

        }



        private ReturnedData Autorization()
        {

            bool isResponseValid = false;

            int selectedMenuItem;

            ReturnedData returnedData = CallbackConsoleMenu("Меню Авторизации", "Выберите пункт", MenuAutorizationText.menuItems);

            do
            {

                if (returnedData.ExecutionStatusCode == ExecutionStatusCode.ExitBeforeCompletion)
                {
                    return returnedData;
                }

                ReturnedData returned = CallbackConsoleMenu("Меню Авторизации", "Выберите пункт", MenuAutorizationText.menuItems);

                isResponseValid = int.TryParse(returned.ReturnedString, out selectedMenuItem);



                //isResponseValid = int.TryParse(CallbackConsoleMenu("Меню Авторизации", "Выберите пункт", MenuAutorizationText.menuItems).ReturnedString, out selectedMenuItem)
                //      && selectedMenuItem >= 0 && selectedMenuItem <= MenuAutorizationText.menuItems.Length;

            } while (!isResponseValid);

            switch (selectedMenuItem)
            {
                case 0:
                    return logining();
                    
                case 1:
                    return AddNewAccaunt(ref accounts);

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

            if (TempAccaunt.Password!=null && TempAccaunt.Password==password)
            {
                CurrentAccaunt = TempAccaunt;
                return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion };
            } 

            return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion };

        }

        private Account FindAccountToLigin(string login ,Account [] accounts)
        {
            for (int i = 0; i < accounts.Length; i++)
            {
                if ((accounts[i].Login)==login)
                {
                    return accounts[i];
                }
            }
                return null;
        }

        private ReturnedData AddNewAccaunt(ref Account[] accounts)
        {
            Account[] tempAccaunts = new Account[accounts.Length + 1];

            Account tempAccaunt = RegistrationNewAccaunt();

            if (tempAccaunt == null)
            {
                return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion };
            }

            for (int i = 0; i < accounts.Length; i++)
            {
                //tempAccaunts[i] = accounts.Length - 1 == i ? tempAccaunt : accounts[i];
                if (accounts.Length - 1 == i)
                {
                    tempAccaunts[i] = accounts.Length - 1 == i ? tempAccaunt : accounts[i];
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
                isResponseValid = int.TryParse(CallbackConsoleMenu("Регистрация Аккаунта", "Выберите пол", MenuAutorizationText.menuItems).ReturnedString, out selectedMenuItem)
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

            return new Account(fio, sex, nicName, age, balance, login, password);
        }

    }
}
