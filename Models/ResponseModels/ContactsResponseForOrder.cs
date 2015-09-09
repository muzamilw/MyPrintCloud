
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class ContactsResponseForOrder{
       
        /// <summary>
        /// List of company Contacts
        /// </summary>
        public IEnumerable<CompanyContact> CompanyContacts { get; set; }

        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }
    }
}
