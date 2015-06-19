
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class ContactResponseForOrder
    {

        /// <summary>
        /// List of company Contacts
        /// </summary>
        public IEnumerable<CompanyContactForOrder> CompanyContacts { get; set; }

        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }
    }
}