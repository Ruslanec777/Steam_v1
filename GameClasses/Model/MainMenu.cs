using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using  Application.Model;
using static Application.Model.SteamClient;
using static Application.Model.MenuActions;


namespace Application.Model
{
    class UsersMenu
    {
        public static void PrintMainMenu()
        {
            int selectedMenuItem = PrintMenu($"Меню авторизованного пользователя +{CurrentAccaunt.NicName}", "Выберите пункт или Esc для выхода", MenuUser.menuItems) ; //почему не могу сделать статический using ?

            switch (selectedMenuItem)
            {
                case 0:


                default:
                    break;
            }

        }
    }
}
