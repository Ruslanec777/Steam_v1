using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Application.Model;
using static Application.Model.SteamClient;
using static Application.Model.MenuActions;
using Application.Enums;
using System.Threading;

namespace Application.Model
{
    class UsersMenu
    {
        public static void PrintMainMenu()
        {
            int selectedMenuItem = PrintMenu($"Меню авторизованного пользователя +{CurrentAccaunt.NicName}", "Выберите пункт или Esc для выхода", MenuUser.menuItems);
            try
            {
                switch (selectedMenuItem)
                {
                    case 0:
                        GameShop.BuyingGame(PrintMenu("Список купленных игр", "Выберете игру", CurrentAccaunt.GamesNames)).PlayTheGame();

                        break;

                    case 1:
                        Game tempGame = GameShop.BuyingGame(PrintMenu("Список игр доступных для покупки", "Выберете игру для покупки", GameStoreMenu.menuItems));
                        if (null != tempGame)
                        {
                            PrintMenu($"Вы купили игру {tempGame.Name}", "Нажмите esc для продолжения");
                        }

                        break;

                    case 2:
                        FinanceActions.PrintFinanceActMenu();
                        break;

                    default:
                        break;
                }
            }
            catch (MenuException)
            {
                throw new MenuException(MenuExceptions.ReturningBack);
            }

        }
    }
}
