using System;

namespace MPC.MIS.Areas.Api.Models
{
    public class GoodsReceivedNoteListView
    {
        public int GoodsReceivedId { get; set; }
        public string Code { get; set; }
        public DateTime? DateReceived { get; set; }
        public string SupplierName { get; set; }
        public double? TotalPrice { get; set; }
    }
}