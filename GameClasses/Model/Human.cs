using GameClasses.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameClasses
{
    public class Human
    {
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Patronymic { get; set; }
        public string Fio
        {
            get { return $"{Surname} {Name} {Patronymic ?? ""}"; }
        }

        public int Age { get; set; }
        public Sex Sex { get; set; }




        public Human(string fio ,int age , Sex sex)
        {
            string[] tempFio = fio.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            Surname = tempFio[0];
            Name = tempFio[1];
            Patronymic = tempFio.Length>2? tempFio[2]: "";

            Age = age;
            Sex = sex;

        }

        public string GetFullName()
        {
            return $"Фамилия: {Surname}\n" +
                   $"Имя: {Name} \n" +
                   $"Отчество: {Patronymic}";
        }
    }
}
