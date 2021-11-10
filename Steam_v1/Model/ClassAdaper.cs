using System;
using SteamClientLab.Model;
using SteamClientLab.Enums;

namespace ClassesProject
{
    partial class Program
    {
        class ClassAdaper
        {

            public static ReturnedData AdapterForSteamClient(string title, string action, params string[] menuItems)
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
                        return new ReturnedData()
                        {
                            ExecutionStatusCode = ExecutionStatusCode.ExitBeforeCompletion
                        };
                    }

                    if (key.Key == ConsoleKey.Enter)

                    {
                        return new ReturnedData()
                        {
                            ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion,
                            ReturnedString = enteredStr
                        };
                    }
                }
            }
        }



    }
}
