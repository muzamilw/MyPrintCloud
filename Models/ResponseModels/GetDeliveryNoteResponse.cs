using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Delivery Note Response Model
    /// </summary>
    public class GetDeliveryNoteResponse
    {
        /// <summary>
        /// Delivery Note
        /// </summary>
        public IEnumerable<DeliveryNote> DeliveryNotes { get; set; }
        
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
        public string HeadNote { get; set; }
        public string FootNote { get; set; }
    }
}
