using Application.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Model
{
    public class Player : Human
    {
        private string NickName { get; set; }
        public Account Account { get; set; }
        public decimal Balance { get; set; }

        private string Login { get; set; }
        private string Pasword { get; set; }

        public string PlayerData
        {
            get { return $"{NickName}  {Account.Id}  {Balance}"; }
        }



        public Player(string nickName, string fio, int age, Sex sex)
            : base(fio, age, sex)
        {
            NickName = nickName;
        }

        public Player(string nickName, string fio, int age, Sex sex, decimal balance)
            : this(nickName, fio, age, sex)
        {
            Balance = balance;
        }

        //public void CreateAccount(string login, string password)
        //{
        //    Account = new Account(login, password);
        //    Login = login;
        //    Pasword = password;
        //    //return Account;
        //}

        public void RemoveAccaunt()
        {
            Account = null;
        }

        public void AddManeyToAccaunt(Account account, decimal maney)
        {
            account.AddMoney(maney);
        }

        public void RemoveMoneyFromAccount(Account account, decimal maney)
        {
            Balance += account.RemoveMoney(maney);
        }

        public string GetPlayerData()
        {
            return $"Ник {NickName} \n" +
                $"Аккаунт: {(Account != null ? Account.Id.ToString() : "Аккаунт отсутствует")} " +
                $"\nБаланс {Balance}";
        }


    }
}
