using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/*

=====================
Создать класс Аккаунт
=====================
Поля: 
id, сумма денег(на аккаунте), логин, пароль, авторизован ли аккаунт, игры

Методы: 
пополнить счет (принимает сумму, на которую пополняет данный аккаунт), списать со счета (принимает на вход сумму, которую нужно списать с аккаунта и вернуть),
Авторизация(принимает логин, пароль, если они равны логину и паролю аккаунта, то авторизация прошла успешно, возвращает true / false).
Добавить игру(Принимает на вход игру и добавляет её в коллекцию игр аккаунта)

Конструкторы:
создать конструктор, который принимает id
создать конструктор, который принимает id и сумму денег

 */

namespace GameClasses
{
    public class Account
    {
        private static long AccauntCount
        { set; get; }

        public long Id { get; private set; }

        public decimal Balance { get; set; }

        public string Login { get; set; }
        public string Password { get; set; }

        public bool IsAuthorized { get; set; }

        public string AccauntData
        {
            get
            {
                return $"{Id} {Balance} {IsAuthorized} {Login}";
            }
            set { }
        }

        public Game[] Games { get; set; }

        public Account(int id)
        {
            Id = id;
        }

        public Account(int id, int money)
            : this(id)
        {
            Balance = money;
        }

        public Account(string login, string password)
        {
            Login = login;
            Password = password;
            Id = AccauntCount;
            AccauntCount++;
        }

        public void AddMoney(decimal money)
        {
            Balance += money;
        }

        public decimal RemoveMoney(decimal money)
        {
            if (Balance > money)
            {
                Balance -= money;
                return money;
            }
            else
            {
                decimal tempMoney = Balance;
                Balance = 0;
                return tempMoney;
            }
        }

        public bool Authorization(string login, string password)
        {
            if (Login == login && Password == password)
            {
                return true;
            }

            return false;
        }
        // нехватает знаний шаблонов
        public void AddGame(Game game)
        {
            Game[] tempGames = new Game[Games.Length + 1];

            for (int i = 0; i < tempGames.Length; i++)
            {
                tempGames[i] = (i == tempGames.Length - 1) ? game : Games[i];
            }

            Games= tempGames;
        }

        public string GetAccauntData()
        {
            return $"ID: {Id}\n" +
                   $"Balance: {Balance}\n" +
                   $"IsAuthorized: {IsAuthorized} \n" +
                   $"Login: {Login}";
        }

        public Game[] RemoveGame(string nameGame, Game[] games)
        {
            Game[] tempArraay = new Game[games.Length - 1];

            bool gameHasBeenDeleted = false;

            for (int i = 0; i < games.Length; i++)
            {
                if (i == games.Length - 1 || !gameHasBeenDeleted)
                {
                    return games;
                }

                if (nameGame == games[i].Name)
                {
                    gameHasBeenDeleted = true;

                    continue;
                }

                tempArraay[i] = games[i];
            }

            return tempArraay;
        }
    }
}
