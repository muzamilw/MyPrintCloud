using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class GoodsReceivedNoteResponseModel
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Goods Received Note
        /// </summary>
        public IEnumerable<GoodsReceivedNoteListView> GoodsReceivedNotesList { get; set; }
    }
}