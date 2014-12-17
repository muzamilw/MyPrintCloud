namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Get Products List View
    /// </summary>
    public class GetProductsListView
    {
        public long ItemId { get; set; }
        public long? EstimateId { get; set; }
        public string ProductName { get; set; }
        public string ProductCategoryName { get; set; }
        public long? ProductCategoryId { get; set; }
        public int? ParentCategoryId { get; set; }
        public double MinPrice { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string IconPath { get; set; }
        public bool? IsEnabled { get; set; }
        public bool? IsSpecialItem { get; set; }
        public bool? IsPopular { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? IsPromotional { get; set; }
        public bool? IsPublished { get; set; }
        public int? IsFinishedGoods { get; set; }
        public string ProductSpecification { get; set; }
        public string CompleteSpecification { get; set; }
        public string TipsAndHints { get; set; }
        public bool? IsArchived { get; set; }
        public long? InvoiceId { get; set; }
        public int? TemplateId { get; set; }
        public int? SortOrder { get; set; }
        public bool? IsMarketingBrief { get; set; }
        public string WebDescription { get; set; }
        public bool? IsQtyRanged { get; set; }
        public int? PriceDiscountPercentage { get; set; }
        public bool? IsTemplateDesignMode { get; set; }
        public double? DefaultItemTax { get; set; }
    }
}
