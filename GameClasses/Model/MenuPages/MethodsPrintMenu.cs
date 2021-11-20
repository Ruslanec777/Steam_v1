using Application.Enums;


namespace Application.Model
{
    public static class MethodsPrintMenu
    {
        public static int PrintMenu(string titleMenu, string actionText, params string[] menuItems)
        {
            bool isResponseValid = false;
            int selectedMenuItem=-1;

            do
            {
                try
                {
                    string responsMenu = SteamClient.CallbackConsoleMenu(titleMenu, actionText, menuItems);

                    isResponseValid = int.TryParse(responsMenu, out selectedMenuItem)
                          && selectedMenuItem >= 0 && selectedMenuItem <= menuItems.Length;
                }
                catch (MenuException)
                {
                    throw new MenuException(MenuExceptions.ExitRequest);
                }

            } while (!isResponseValid);
            return selectedMenuItem;
        }

        public static decimal PrintMenuMoneyRequest(string titleMenu, string actionText)
        {
            bool isResponseValid = false;
            decimal inputingNumber= -1;

            do
            {
                try
                {
                    string responsMenu = SteamClient.CallbackConsoleMenu(titleMenu, actionText);

                    isResponseValid = decimal.TryParse(responsMenu, out inputingNumber) && inputingNumber >= 0 ;
                }
                catch (MenuException)
                {
                    throw new MenuException(MenuExceptions.ExitRequest);
                }

            } while (!isResponseValid);
            return inputingNumber;
        }


    }



}

