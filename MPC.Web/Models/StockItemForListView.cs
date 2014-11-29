namespace MPC.MIS.Models
{
    /// <summary>
    /// Stock Item For Lit View
    /// </summary>
    public class StockItemForListView
    {
        /// <summary>
        /// Stock Item ID
        /// </summary>
        public int StockItemId { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Item Weight
        /// </summary>
        public int? ItemWeight { get; set; }

        /// <summary>
        /// Weight Unit Name
        /// </summary>
        public string WeightUnitName { get; set; }

        /// <summary>
        /// Per Qty Qty(/1000)
        /// </summary>
        public double? PerQtyQty { get; set; }

        /// <summary>
        /// Item Colour(Code)
        /// </summary>
        public string FlagColor { get; set; }

        /// <summary>
        /// Category Name
        /// </summary>
        public string CategoryName { get; set; }

        /// <summary>
        /// Sub Category Name
        /// </summary>
        public string SubCategoryName { get; set; }

        /// <summary>
        /// Full Category Name(Category and Sub Category Name)
        /// </summary>
        public string FullCategoryName { get; set; }

        /// <summary>
        /// Supplier Company Name
        /// </summary>
        public string SupplierCompanyName { get; set; }
    }
}