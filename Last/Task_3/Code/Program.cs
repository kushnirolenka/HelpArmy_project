using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpArmyDB
{
    class Program
    {
        static void Main(string[] args)
        {
            //DB_connect db_connect = new DB_connect();
            //db_connect.SetupingScript();
            TablesDB tablesDB = new TablesDB();
            tablesDB.FillTables();
            tablesDB.PrintAllTables();
        }
    }
}
