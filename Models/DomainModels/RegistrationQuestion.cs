using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class RegistrationQuestion
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }

        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}
