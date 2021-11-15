using Application.Enums;
using System.Threading;
using static System.Console;
using Application.Model;

namespace ClassesProject
{
    partial class Program
    {
        static void Main(string[] args)
        {
            SteamClient st = new SteamClient(ClassAdaper.AdapterForSteamClient);
            st.Start();

            return;
        }
    }
}
