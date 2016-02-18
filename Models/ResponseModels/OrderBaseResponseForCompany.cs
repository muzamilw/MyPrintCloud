using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Order Base Response For Company
    /// </summary>
    public class OrderBaseResponseForCompany
    {
        /// <summary>
        /// Contacts
        /// </summary>
        public IEnumerable<CompanyContact> CompanyContacts { get; set; }

        /// <summary>
        /// Addresses
        /// </summary>
        public IEnumerable<Address> CompanyAddresses { get; set; }
        
        /// <summary>
        /// Tax Rate
        /// </summary>

        public double? TaxRate { get; set; }

        /// <summary>
        /// Job Manager Id
        /// </summary>
        public Guid? JobManagerId { get; set; }

        public bool IsStoreLive { get; set; }
        public bool IsMisOrdersCountReached { get; set; }
        public bool IsWebOrdersCountReached { get; set; }
        public IEnumerable<DeliveryNote> DeliveryNotes { get; set; }
    }
}
