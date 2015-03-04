﻿using System.Collections.Generic;
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
    }
}