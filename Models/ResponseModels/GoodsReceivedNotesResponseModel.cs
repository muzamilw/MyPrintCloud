using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Goods Received Note Response Model
    /// </summary>
    public class GoodsReceivedNotesResponseModel
    {
        /// <summary>
        /// Purchases
        /// </summary>
        public IEnumerable<GoodsReceivedNote> GoodsReceivedNotes { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
