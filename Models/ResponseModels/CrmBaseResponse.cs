using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class CrmBaseResponse
    {
        public IEnumerable<SystemUser> SystemUsers { get; set; }
        public IEnumerable<CompanyContactRole> CompanyContactRoles { get; set; }
        public IEnumerable<SectionFlag> SectionFlags { get; set; }
        public IEnumerable<RegistrationQuestion> RegistrationQuestions { get; set; }
        public IEnumerable<Country> Countries { get; set; }
        public IEnumerable<State> States { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public long? DefaultCountryId { get; set; }
    }
}
