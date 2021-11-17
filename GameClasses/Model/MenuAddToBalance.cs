using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    class MenuAddToBalance
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


        //static internal string[] menuItems = {
        //    "Пополние счета",
        //    "Введите сумму пополнения"};
        //    public static int PrintMenu()
        //    {

        //        return new string[] ={ "wewe" };
        //    }
    }
}
