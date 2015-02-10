using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Calendar Base Response
    /// </summary>
    public class CalendarBaseResponse
    {
        public IEnumerable<SystemUser> SystemUsers { get; set; }
        public IEnumerable<CompanyContact> CompanyContacts { get; set; }
        //public IEnumerable<p> CompanyContacts { get; set; }
    }
}
