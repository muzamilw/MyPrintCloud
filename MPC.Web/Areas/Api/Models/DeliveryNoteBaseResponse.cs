using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// web model
    /// </summary>
    public class DeliveryNoteBaseResponse
    {
        /// <summary>
        /// Section Flags
        /// </summary>
        public IEnumerable<SectionFlagDropDown> SectionFlags { get; set; }

        /// <summary>
        /// System Users
        /// </summary>
        public IEnumerable<SystemUserDropDown> SystemUsers { get; set; }


        /// <summary>
        /// Delivery Carriers
        /// </summary>
        public IEnumerable<DeliveryCarrier> DeliveryCarriers { get; set; }
    }
}