using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace HelpArmyDB
{
    public class OrganizationRepository
    {
        private readonly ConnectEF connectEF = new ConnectEF();
        public virtual void Create(Organization organization)
        {
            bool organizationExist = connectEF.organization.Any(a => a.email_organization == organization.email_organization);

            if (!organizationExist && organization != null)
            {
                connectEF.organization.Add(organization);
                connectEF.SaveChanges();
            }

        }
        public virtual Organization GetId(int id)
        {
            return connectEF.organization.FirstOrDefault(a => a.id_organization == id);
        }
        public virtual Organization GetEmail(string email)
        {
            return connectEF.organization.FirstOrDefault(a => a.email_organization == email);
        }
        public virtual List<Organization> GetAll()
        {
            return connectEF.organization.ToList();
        }
        public virtual void Update(Organization organization)
        {
            connectEF.Entry(organization).State = EntityState.Modified;
            connectEF.SaveChanges();
        }
    }
}
