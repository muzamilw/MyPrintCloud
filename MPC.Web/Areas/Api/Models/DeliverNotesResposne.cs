using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class DeliverNotesResposne
    {
        /// <summary>
        /// Delivery Note
        /// </summary>
        public IEnumerable<DeliveryNote> DeliveryNotes { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}