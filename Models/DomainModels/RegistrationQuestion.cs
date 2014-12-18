using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    public class RegistrationQuestion
    {
        public int QuestionId { get; set; }
        public string Question { get; set; }

        public virtual ICollection<CompanyContact> CompanyContacts { get; set; }
    }
}
