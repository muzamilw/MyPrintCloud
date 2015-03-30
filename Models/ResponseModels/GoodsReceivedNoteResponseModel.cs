using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class GoodsReceivedNoteResponseModel
    {
        /// <summary>
        /// Goods Received Notes
        /// </summary>
        public IEnumerable<GoodsReceivedNote> GoodsReceivedNotes { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
