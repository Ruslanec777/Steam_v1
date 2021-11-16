using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Application.Model
{
   public class MenuAutorizationText
    {
        static readonly string titlMenu = "Меню Авторизации";

        static readonly string ActionForMenu = "Выберите пункт меню";
        static internal string HeaderOfMenu
        {
            get
            {
                return $"{titlMenu} \n" +
                       $"\n" +
                       $"{ActionForMenu}";
            }
        }
        static internal string[] menuItems = { "Войти в Аккаунт", "Регистрация" };
    }

    class MenuLoginForLogining
    {
        static readonly string titlMenu = "Меню Входа в Аккаунт";

        static readonly string ActionForMenu = "Введите Логин";
        static internal string HeaderOfMenu
        {
            get
            {
                return $"{titlMenu} \n" +
                       $"\n" +
                       $"{ActionForMenu}";
            }
        }
        static internal string[] menuItems = Array.Empty<string>();
    }

    class MenuPasworddForLogining
    {
        static readonly string titlMenu = "Меню Входа в Аккаунт";

        static readonly string ActionForMenu = "Введите Пароль";
        static internal string HeaderOfMenu
        {
            get
            {
                return $"{titlMenu} \n" +
                       $"\n" +
                       $"{ActionForMenu}";
            }
        }
        static internal string[] menuItems = null;
    }

    class MenuLoginForRegistration
    {
        static readonly string titlMenu = "Меню Регистрации";

        static readonly string ActionForMenu = "Введите Логин";
        static internal string HeaderOfMenu
        {
            get
            {
                return $"{titlMenu} \n" +
                       $"\n" +
                       $"{ActionForMenu}";
            }
        }
        static internal string[] menuItems = null;
    }

    class MenuPasworddForRegistration
    {
        static readonly string titlMenu = "Меню Регистрации";

        static readonly string ActionForMenu = "Введите Пароль";
        static internal string HeaderOfMenu
        {
            get
            {
                return $"{titlMenu} \n" +
                       $"\n" +
                       $"{ActionForMenu}";
            }
        }
        static internal string[] menuItems = null;
    }

    class MenuSexSelect
    {
        static readonly string titlMenu = "Меню Авторизации";

        static readonly string ActionForMenu = "Выберите пункт меню";
        static internal string HeaderOfMenu
        {
            get
            {
                return $"{titlMenu} \n" +
                       $"\n" +
                       $"{ActionForMenu}";
            }
        }
        static internal string[] menuItems = { "Мужчина", "Женщина" };
    }

    class MenuUser
    {
        static readonly string titlMenu = $"Пользователь";

        static internal readonly string ActionForMenu = "Выберите пункт меню";
        static internal string HeaderOfMenu
        {
            get
            {
                return $"{titlMenu} \n" +
                       $"\n" +
                       $"{ActionForMenu}";
            }
        }
        static internal string[] menuItems = {
            "Играть в игру",
            "Список игр доступных для покупки" ,
            "Управление счётом",
            "Выйти из аккаунта" ,
            "Выйти из Steam" };
    }

    class MenuTextAnyKey
    {

    }


     public static class MenuActions
    {
        public static int PrintMenu(string titleMenu, string actionText, params string[] menuItems)
        {
            bool isResponseValid = false;
            int selectedMenuItem;

            do
            {
                try
                {
                    string responsMenu = SteamClient.CallbackConsoleMenu(titleMenu, actionText, menuItems);

                    isResponseValid = int.TryParse(responsMenu, out selectedMenuItem)
                          && selectedMenuItem >= 0 && selectedMenuItem <= MenuAutorizationText.menuItems.Length;
                }
                catch (MenuException)
                {
                    throw new MenuException(MenuExceptions.ExitRequest);
                }

            } while (!isResponseValid);
            return selectedMenuItem;
        }
    }



}

