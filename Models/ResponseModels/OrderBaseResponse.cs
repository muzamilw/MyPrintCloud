using System;
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Order Base Response
    /// </summary>
    public class OrderBaseResponse
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
        /// Pipeline Sources
        /// </summary>
        public IEnumerable<PipeLineSource> PipeLineSources { get; set; }

        /// <summary>
        /// Pipeline Product
        /// </summary>
        public IEnumerable<PipeLineProduct> PipeLineProducts { get; set; }

        /// <summary>
        /// Payment Methoda
        /// </summary>
        public IEnumerable<PaymentMethod> PaymentMethods { get; set; }

        /// <summary>
        /// Organisation
        /// </summary>
        public Organisation Organisation { get; set; }


        /// <summary>
        /// Chart Of Accounts
        /// </summary>
        public IEnumerable<ChartOfAccount> ChartOfAccounts { get; set; }

        /// <summary>
        /// Cost Centers
        /// </summary>
        public IEnumerable<CostCentre> CostCenters { get; set; }

        /// <summary>
        /// Logged In User
        /// </summary>
        public Guid LoggedInUser { get; set; }

        /// <summary>
        /// Head Notes
        /// </summary>
        public string HeadNotes { get; set; }

        /// <summary>
        /// Foot Notes
        /// </summary>
        public string FootNotes { get; set; }
        public IEnumerable<DeliveryCarrier> DeliveryCarriers { get; set; }
    }
}
