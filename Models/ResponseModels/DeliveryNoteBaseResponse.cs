
using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// domain side
    /// </summary>
    public class DeliveryNoteBaseResponse
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

       
    }
}
