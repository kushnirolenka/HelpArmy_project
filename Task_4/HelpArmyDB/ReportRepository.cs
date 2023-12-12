using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpArmyDB
{
    class ReportRepository
    {
        private readonly ConnectEF connectEF = new ConnectEF();
        public virtual void Create(Report report)
        {
            connectEF.report.Add(report);
            connectEF.SaveChanges();

        }
        public virtual Report Get(int id)
        {
            return connectEF.report.FirstOrDefault(a => a.id_report == id);
        }
        public virtual List<Report> GetAll()
        {
            return connectEF.report.ToList();
        }
        public virtual void Update(Report report)
        {
            connectEF.Entry(report).State = EntityState.Modified;
            connectEF.SaveChanges();
        }
    }
}
