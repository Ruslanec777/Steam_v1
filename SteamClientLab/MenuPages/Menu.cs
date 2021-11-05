using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SteamClientLab.MenuPages
{
    class MenuAutorizationText
    {
        static readonly string titlMenu = "Меню Авторизации";

        static readonly string ActionForMenu ="Выберите пункт меню";
        static internal string HeaderOfMenu
        {
            get
            {
                return $"{titlMenu} \n" +
                       $"\n" +
                       $"{ActionForMenu}";
            }
        }
        static internal string[]  menuItems= { "Войти в Аккаунт" ,"Регистрация" ,"Выход" };
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



}

