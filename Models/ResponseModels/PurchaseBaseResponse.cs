using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Purchase Base Domain Response
    /// </summary>
    public class PurchaseBaseResponse
    {
        /// <summary>
        /// Section Flags
        /// </summary>
        public IEnumerable<SectionFlag> SectionFlags { get; set; }

        /// <summary>
        /// System Users
        /// </summary>
        public IEnumerable<SystemUser> SystemUsers { get; set; }

        /// <summary>
        /// Delivery Carriers
        /// </summary>
        public IEnumerable<DeliveryCarrier> DeliveryCarriers { get; set; }

        /// <summary>
        /// Currency Symbol
        /// </summary>
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Logged In User
        /// </summary>
        public Guid LoggedInUser { get; set; }
    }
}
