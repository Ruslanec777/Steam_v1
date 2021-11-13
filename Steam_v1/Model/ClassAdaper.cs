using System;
using SteamClientLab.Model;
using SteamClientLab.Enums;

namespace ClassesProject
{
    partial class Program
    {
        class ClassAdaper
        {

            public static string AdapterForSteamClient(string title, string action, params string[] menuItems)
            {
                Console.WriteLine("reset bag console\n"); // Иногда консоль не отображает первый символ
                Console.Clear();
                Console.WriteLine(title);
                Console.WriteLine(action);

                for (int i = 0; i < menuItems.Length; i++)
                {
                    Console.WriteLine($"{i}. {menuItems[i]}");
                }

                string enteredStr = string.Empty;

                while (true)
                {
                    ConsoleKeyInfo key = Console.ReadKey();

                    enteredStr += key.KeyChar;

                    if (key.Key == ConsoleKey.Escape)
                    {
                        throw new MenuException( MenuExceptions.ReturningBack ," Нажат Esc");
                    }

                    if (key.Key == ConsoleKey.Enter)

                    {
                        return enteredStr.Remove(enteredStr.Length-1);
                    }
                }
            }
        }
    }
}
