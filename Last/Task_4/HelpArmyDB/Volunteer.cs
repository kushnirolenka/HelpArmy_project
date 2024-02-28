using Abp.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpArmyDB
{
    public class Volunteer
    {
        int id;
        string name;
        string surname;
        string city;
        string email;
        string password;

        public Volunteer(int id, string name, string surname, string city, string email, string password)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.city = city;
            this.email = email;
            this.password = password;
        }
        public Volunteer()
        {
            this.id = 0;
            this.name = "";
            this.surname = "";
            this.city = "";
            this.email = "";
            this.password = "";
        }

        public int id_volunteer { get => id; set => id = value; }
        public string name_volunteer { get => name; set => name = value; }
        public string surname_volunteer { get => surname; set => surname = value; }
        public string city_volunteer { get => city; set => city = value; }
        public string email_volunteer { get => email; set => email = value; }
        public string password_volunteer { get => password; set => password = value; }

        public bool IsTransient()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            string str = "";
            str = id + "\n";
            str = name + "\n";
            str = surname + "\n";
            str += city + "\n";
            str += email + "\n";
            str += password + "\n";


            return str;
        }
    }
}
