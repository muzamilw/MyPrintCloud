namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Product Category Item WebApi Model
    /// </summary>
    public class ProductCategoryItem
    {
        public long ProductCategoryItemId { get; set; }
        public long? CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long? ItemId { get; set; }

        /// <summary>
        /// True if selected for Product
        /// </summary>
        public bool? IsSelected { get; set; }
    }
}
