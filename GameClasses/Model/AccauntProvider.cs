using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Model.MenuActions;
using static Application.Model.SteamClient;

namespace Application.Model
{
    public class AccauntProvider
    {

        public static void Autorization()
        {
            int selectedMenuItem = PrintMenu("Меню Авторизации", "Выберите пункт или Esc для выхода", MenuAutorizationText.menuItems);

            try
            {
                switch (selectedMenuItem)
                {
                    case 0:
                        logining();

                        return;

                    case 1:
                        RegistrationAccaunt();

                        return;

                    default:
                        throw new MenuException(MenuExceptions.ReturningBack);
                }
            }
            catch (MenuException)
            {
                throw new MenuException(MenuExceptions.ReturningBack);
            }
        }



        private static Account logining()
        {
            string login;
            string password;

            login = CallbackConsoleMenu("Вход в Аккаунт", "Введите логин");
            password = CallbackConsoleMenu("Вход в Аккаунт", "Введите пароль");

            Account TempAccaunt = FindAccountToLigin(login);

            if (TempAccaunt != null && TempAccaunt.Password == password)
            {
                SteamClient.CurrentAccaunt = TempAccaunt;
                SteamClient.CurrentAccaunt.IsAuthorized = true;

                SteamClient.CallbackConsoleMenu("Пользавотель вошел в аккаунт:", TempAccaunt.GetAccauntData());

                return TempAccaunt;
            }

            return null;
        }

        private static Account FindAccountToLigin(string login)
        {
            Account account = SteamClient.accounts.FirstOrDefault(i => i.Login == login);
            if (account == null)
            {
                return null;
            }
            return account;
        }

        private static Account RegistrationAccaunt()
        {
            return AddNewAccaunt(CreateAccauntInstance());
        }

        private static Account AddNewAccaunt(Account account)
        {
            if (account == null)
            {
                return null;
            }
            accounts.Add(account);

            return account;
        }

        private static Account CreateAccauntInstance()
        {
            string fio = string.Empty;
            Sex sex = Sex.Man;
            string nicName = string.Empty;
            int age = 0;
            decimal balance = 0;
            string login = string.Empty;
            string password = string.Empty;

            login = InputingUniqLogin();

            password = CallbackConsoleMenu("Регистрация Аккаунта", "Введите пароль");

            fio = InputingFio();

            sex = ChooseSex();

            nicName = CallbackConsoleMenu("Регистрация Аккаунта", "Введите Ник для игры");

            age = InputingAge();

            CurrentAccaunt = new Account(fio, sex, nicName, age, balance, login, password); // заносим вновь созданный аккаунт в текущий , переделать( несколько действий в методе)

            return CurrentAccaunt;
        }

        private static int InputingAge()
        {
            bool isResponseValid = false;
            int age;

            do
            {
                isResponseValid = int.TryParse(CallbackConsoleMenu("Регистрация Аккаунта", "Введите возраст"), out age) && age >= 0 && age <= 130;
            } while (!isResponseValid);

            return age;
        }

        private static string InputingUniqLogin()
        {
            string login;
            do
            {
                login = CallbackConsoleMenu("Регистрация Аккаунта", "Введите логин");

            } while (null != FindAccountToLigin(login));
            return login;
        }

        private static string InputingFio()
        {
            string fio;
            do
            {
                fio = CallbackConsoleMenu("Регистрация Аккаунта", "ФИО через пробел");

            } while (!(fio.Split(" ").Length >= 2 && fio.Split(" ").Length <= 3));

            return fio;
        }

        private static Sex ChooseSex()
        {
            bool isResponseValid = false;
            int selectedMenuItem;

            do
            {
                string responsString = CallbackConsoleMenu("Регистрация Аккаунта", "Выберите пол", MenuSexSelect.menuItems);

                isResponseValid = int.TryParse(responsString, out selectedMenuItem)
                && selectedMenuItem >= 0 && selectedMenuItem <= 1;

            } while (!isResponseValid);
            return (Sex)selectedMenuItem;
        }

        public static Account AdminAccountInitializer()
        {
            return AddNewAccaunt(new Account("Админ Админович Админовский", Sex.Man, "Admin", 100, 1000000, "admin", "1234"));
        }

    }
}
