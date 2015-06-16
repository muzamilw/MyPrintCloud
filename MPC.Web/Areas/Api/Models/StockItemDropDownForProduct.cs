namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Stock Item used in Product for default selection
    /// </summary>
    public class StockItemDropDownForProduct
    {
        /// <summary>
        /// Stock Item ID
        /// </summary>
        public long StockItemId { get; set; }

        /// <summary>
        /// Item Code
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Alternate Name
        /// </summary>
        public string AlternateName { get; set; }

        /// <summary>
        /// Package Quantity
        /// </summary>
        public double? PackageQty { get; set; }

        /// <summary>
        /// In Stock
        /// </summary>
        public double? InStock { get; set; }

        /// <summary>
        /// Allocated
        /// </summary>
        public double? Allocated { get; set; }

    }
}