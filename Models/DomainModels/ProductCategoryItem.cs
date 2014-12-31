namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Product Category Item Domain Model
    /// </summary>
    public class ProductCategoryItem
    {
        public long ProductCategoryItemId { get; set; }
        public long? CategoryId { get; set; }
        public long? ItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }
    }
}
