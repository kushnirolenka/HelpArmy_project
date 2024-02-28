using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpArmyDB
{
    public class Report
    {
        int id;
        string type;
        DateTime start_report;
        DateTime end_report;
        int org_id;

        public int id_report { get => id; set => id = value; }
        public string type_donation_report { get => type; set => type = value; }
        public DateTime date_start_report { get => start_report; set => start_report = value; }
        public DateTime date_end_report { get => end_report; set => end_report = value; }
        public int id_organization { get => org_id; set => org_id = value; }

        public override bool Equals(object obj)
        {
            return obj is Report report &&
                   id == report.id &&
                   type == report.type &&
                   start_report == report.start_report &&
                   end_report == report.end_report &&
                   org_id == report.org_id;
        }
        public override string ToString()
        {
            string str = "";
            str = id + "\n";
            str = type + "\n";
            str = start_report.Date + "\n";
            str = end_report.Date + "\n";
            str += org_id + "\n";
            return str;
        }
    }
}
