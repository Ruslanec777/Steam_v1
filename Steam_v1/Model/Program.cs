using Application.Enums;
using GameClasses;
using GameClasses.Enums;
using System;
using System.Threading;
using static System.Console;
using SteamClientLab.Model;
using SteamClientLab.Enums;

namespace ClassesProject
{
    class Program
    {
        static void Main(string[] args)
        {
            SteamClient st = new SteamClient(AdapterForSteamClient);
            st.Start();

            return;
        }

        public static ReturnedData AdapterForSteamClient(string title, string action, params string[] menuItems)
        {
            Console.Clear();
            Console.WriteLine(title);
            Console.WriteLine(action );

            for (int i = 0; i < menuItems.Length; i++)
            {
                Console.WriteLine($"{i}. {menuItems[i]}");
            }

            string enteredStr=string.Empty;

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

        //public static ReturnedData AdapterForSteamClient(string title, string action)
        //{
        //    Console.Clear();
        //    Console.WriteLine(title);
        //    Console.WriteLine(action);

        //    return new ReturnedData() { ExecutionStatusCode = ExecutionStatusCode.CorrectCompletion, ReturnedString = Console.ReadLine() };
        //}

    }
}
