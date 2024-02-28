using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HelpArmyDB
{
    public class DonateRepository
    {
        private readonly ConnectEF connectEF = new ConnectEF();
        public virtual void Create(Donate donate)
        {
            connectEF.donation.Add(donate);
            connectEF.SaveChanges();

        }
        public virtual Donate Get(int id)
        {
            return connectEF.donation.FirstOrDefault(a => a.id_donation == id);
        }

        public virtual List<Donate> GetAll()
        {
            return connectEF.donation.ToList();
        }
        public virtual void Update(Donate donate, int id)
        {
            var result = connectEF.donation.SingleOrDefault(b => b.id_donation == id);

            if (result != null)
            {

                result.name_donation = donate.name_donation;
                result.number_donation = donate.number_donation;
                result.object_donation = donate.object_donation;
                result.photo_donation = donate.photo_donation;
                result.sum_donation = donate.sum_donation;
                result.type_donation = donate.type_donation;
                result.category_donation = donate.category_donation;
                result.date_donation = donate.date_donation;
                connectEF.SaveChanges();
            }
        }
    }
}
