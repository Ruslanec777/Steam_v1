using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    class FinanceActions
    {
        public static int PrintFinanceActMenu()
        {
            int selectedMenuItem= MenuActions.PrintMenu($"Меню управления счетом \n" +
    $"На вашем балансе находится {SteamClient.CurrentAccaunt.Balance}", "Выберите действие", MenuFinanc.menuItems);

            switch (selectedMenuItem)
            {
                case 0:

                    break;
                default:
                    break;
            }
            return 0;
        }
    }
}
