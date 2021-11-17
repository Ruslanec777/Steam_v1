using Application.Enums;
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

namespace Application.Model
{
    public class Account
    {
        public string Fio { get; set; }
        public Sex Sex { get; set; }
        public string NicName { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public int Age { get; set; }
        private static long AccauntCount
        { set; get; }
        public long Id { get; private set; }
        public decimal Balance { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAuthorized { get; set; }

        public Account(string fio, Sex sex, string nicName, int age, decimal balance, string login, string password)
        {
            Fio = fio;
            Sex = sex;
            NicName = nicName;
            Age = age;
            Balance = balance;
            Login = login;
            Password = password;

            ParsingFio(Fio);
        }

        public string AccauntData
        {
            get
            {
                return $"{Id} {Balance} {Login}";
            }
            set { }
        }

        public Game[] Games { get; set; } = Array.Empty<Game>();

        public  string[] GamesNames
        {
            get
            {
                string[] tempSting = new string[Games.Length];

                for (int i = 0; i < Games.Length; i++)
                {
                    tempSting[i] = Games[i].Name;
                }
                return tempSting;
            }
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

        private void ParsingFio(string fio)
        {
            string[] tempFio = fio.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            Surname = tempFio[0];
            Name = tempFio[1];
            Patronymic = tempFio.Length > 2 ? tempFio[2] : "";
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
                tempGames[i] = i == tempGames.Length - 1 ? game : Games[i];
            }

            Games = tempGames;
        }

        public string GetAccauntData()
        {
            return $"ID: {Id}\n" +
                   $"NicName: {NicName} \n" +
                   $"Surname: {Surname} \n" +
                   $"Name: {Name} \n" +
                   $"Patronymic: {Patronymic} \n" +
                   $"Age: {Age} \n" +
                   $"Login: {Login}\n" +
                   $"Password: {Password} \n" +
                   $"Balance: {Balance}\n" +
                   $"IsAuthorized: {IsAuthorized} \n";
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
