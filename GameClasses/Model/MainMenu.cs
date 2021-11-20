using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Application.Model.SteamClient;
using static Application.Model.MethodsPrintMenu;
using Application.Enums;
using System.Threading;
using Application.Model.Actions;

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
                        CurrentAccaunt.Games[PrintMenu("Список купленных игр", "Выберете игру", CurrentAccaunt.GamesNames)].PlayTheGame();

                        break;

                    case 1:
                        Game tempGame = GameShop.BuyingGame(PrintMenu("Список игр доступных для покупки", "Выберете игру для покупки", GameStoreMenu.menuItems));
                        if (null != tempGame)
                        {
                            PrintMenu($"Вы купили игру {tempGame.Name}", "Нажмите esc для продолжения");
                        }

                        break;

                    case 2:
                        FinanceActions.OutPutFinanceActMenu();

                        break;

                    case 3:
                        SteamClient.CurrentAccaunt=null;

                        break;

                    case 4:
                        return ;

                        break;

                    default:
                        break;
                }
            }
            catch (MenuException exception)
            {
                if (exception.ErrorCode==MenuExceptions.ExitRequest)
                {
                    throw;
                }
                throw new MenuException(MenuExceptions.ReturningBack);
            }




        }
    }
}
