using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model.Actions
{
    class FinanceActions
    {
        public static int OutPutFinanceActMenu()
        {
            int selectedMenuItem = MethodsPrintMenu.PrintMenu($"Меню управления счетом \n" +
    $"На вашем балансе находится {SteamClient.CurrentAccaunt.Balance}", "Выберите действие", MenuFinanc.menuItems);

            switch (selectedMenuItem)
            {
                case 0:
                    FinanceActionsAdd.PrintAddBalancMenu();                
                    break;

                case 1:
                    FinanceActionsAdd.PrintRemoveBalancMenu();
                    break;

                default:
                    break;
            }
            return 0;
        }
    }
}
