using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Stock Update History API Model
    /// </summary>
    public class ItemStockUpdateHistory
    {
        public int StockHistoryId { get; set; }
        public int? LastModifiedQty { get; set; }
        public int? ModifyEvent { get; set; }
        public int? LastModifiedBy { get; set; }
        public string LastModifiedByName { get; set; }
        public string Action { get; set; }
        public DateTime? LastModifiedDate { get; set; }
    }
}