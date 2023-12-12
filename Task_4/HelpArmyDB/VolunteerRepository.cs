using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HelpArmyDB
{
    public class VolunteerRepository
    {
        private readonly ConnectEF connectEF = new ConnectEF();
        public virtual void Create(Volunteer volunteer)
        {
            bool volunteerExist = connectEF.volunteer.Any(a => a.email_volunteer == volunteer.email_volunteer);

            if (!volunteerExist && volunteer != null)
            {
                connectEF.volunteer.Add(volunteer);
                connectEF.SaveChanges();
            }
           
        }
        public virtual Volunteer GetId(int id)
        {
            return connectEF.volunteer.FirstOrDefault(a => a.id_volunteer == id);
        }
        public virtual Volunteer GetEmail(string email)
        {
            return connectEF.volunteer.FirstOrDefault(a => a.email_volunteer == email);
        }
        public virtual List<Volunteer> GetAll()
        {
            return connectEF.volunteer.ToList();
        }
        public virtual void Update(Volunteer volunteer)
        {
            connectEF.Entry(volunteer).State = EntityState.Modified;
            connectEF.SaveChanges();
        }
    }
}
