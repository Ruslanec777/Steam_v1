using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Application.Model
{
    public  class SteamClient
    {
        public  delegate string DelegateMethodConsolMenu(string titl, string action, params string[] titlAndMenuItems);
        public static DelegateMethodConsolMenu CallbackConsoleMenu { get; set; }
        public static Account CurrentAccaunt { get; set; } = null;

        // private Account[] accounts = Array.Empty<Account>(); 
        public static List<Account> accounts = new List<Account>();

        public  SteamClient(DelegateMethodConsolMenu externalMethodPrintMenu)
        {
            CallbackConsoleMenu = externalMethodPrintMenu;
        }

        /// <summary>
        /// Метод Запуска SteamClient
        /// </summary>
        /// <param name="externalMethod">ссылка на метод который способен взаимодействует с SteamClient</param>
        public  void Start()
        {
            AccauntProvider.AdminAccountInitializer();

            while (true)
            {
                try
                {
                    while (CurrentAccaunt == null)
                    {
                        AccauntProvider.Autorization();
                    }

                    do
                    {
                        UsersMenu.PrintMainMenu();

                    } while (CurrentAccaunt != null);
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
        }
    }
}
