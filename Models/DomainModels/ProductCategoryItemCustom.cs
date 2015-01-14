namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Product Category Item Wrapper Model
    /// </summary>
    public class ProductCategoryItemCustom : ProductCategoryItem
    {
        /// <summary>
        /// True if selected for Product
        /// </summary>
        public bool? IsSelected { get; set; }
    }
}
