using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpArmyDB
{
    public class Organization
    {
        int id;
        string name;
        string city;
        string email;
        string password;

        public Organization(int id, string name, string city, string email, string password)
        {
            this.id = id;
            this.name = name;
            this.city = city;
            this.email = email;
            this.password = password;
        }
        public Organization()
        {
            this.id = 0;
            this.name = "";
            this.city = "";
            this.email = "";
            this.password = "";
        }

        public int id_organization { get => id; set => id = value; }
        public string name_organization { get => name; set => name = value; }
        public string city_organization { get => city; set => city = value; }
        public string email_organization { get => email; set => email = value; }
        public string password_organization { get => password; set => password = value; }
        public override string ToString()
        {
            string str = "";
            str = id + "\n";
            str = name + "\n";
            str += city + "\n";
            str += email + "\n";
            str += password + "\n";


            return str;
        }
    }
}
