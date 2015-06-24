using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Order Base Response For Company Web Api Model
    /// </summary>
    public class OrderBaseResponseForCompany
    {
        /// <summary>
        /// Contacts
        /// </summary>
        public IEnumerable<CompanyContactDropDownForOrder> CompanyContacts { get; set; }

        /// <summary>
        /// Addresses
        /// </summary>
        public IEnumerable<AddressDropDown> CompanyAddresses { get; set; }

        /// <summary>
        /// Tax Rate
        /// </summary>

        public double? TaxRate { get; set; }
        /// <summary>
        /// Job Manager Id
        /// </summary>
        public Guid? JobManagerId { get; set; }
    }
}