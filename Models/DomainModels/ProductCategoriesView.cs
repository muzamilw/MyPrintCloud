namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Product Categories View Domain Model
    /// </summary>
    public class ProductCategoriesView
    {
        public long? ProductCategoryId { get; set; }
        public string TemplateDesignerMappedCategoryName { get; set; }
        public long ItemId { get; set; }
        public double MinPrice { get; set; }
        public string CategoryName { get; set; }
        public long? CompanyId { get; set; }
        public double? DefaultItemTax { get; set; }
    }
}
