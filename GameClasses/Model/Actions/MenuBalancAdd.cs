using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model.Actions
{
    class FinanceActionsAdd
    {
        public static void PrintAddBalancMenu()
        {
            decimal moneyToAdd= MethodsPrintMenu.PrintMenuMoneyRequest ($"Пополнение счёта пользователя {SteamClient.CurrentAccaunt.Name}", "Введите сумму");

            SteamClient.CurrentAccaunt.AddMoney(moneyToAdd);
        }

        public static void PrintRemoveBalancMenu()
        {
            decimal moneyToAdd = MethodsPrintMenu.PrintMenuMoneyRequest($"Пополнение счёта пользователя {SteamClient.CurrentAccaunt.Name}", "Введите сумму");

            SteamClient.CurrentAccaunt.RemoveMoney(moneyToAdd);
        }
    }
}
