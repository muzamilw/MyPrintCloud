using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Item Stock Update History Domain Model
    /// </summary>
    public class ItemStockUpdateHistory
    {
        public int StockHistoryId { get; set; }
        public int? ItemId { get; set; }
        public int? LastModifiedQty { get; set; }
        public int? ModifyEvent { get; set; }
        public int? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public int? OrderID { get; set; }
        public long? StockItemId { get; set; }
        public virtual StockItem StockItem { get; set; }
    }
}
