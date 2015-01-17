namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Get Category Product Domain Model
    /// </summary>
    public class GetCategoryProduct
    {
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public bool? isQtyRanged { get; set; }
        public long? EstimateId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductCategoryName { get; set; }
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
        public int? ProductType { get; set; }
        public string ProductSpecification { get; set; }
        public string CompleteSpecification { get; set; }
        public bool? IsArchived { get; set; }
        public int? SortOrder { get; set; }
        public long? OrganisationId { get; set; }
        public string WebDescription { get; set; }
        public int? PriceDiscountPercentage { get; set; }
        public bool? isTemplateDesignMode { get; set; }
        public double? DefaultItemTax { get; set; }
        public bool? isUploadImage { get; set; }

        public bool? isMarketingBrief { get; set; }

        public long ProductCategoryId { get; set; }
        public long? TemplateId { get; set; }

        public int? DesignerCategoryId { get; set; }
    }
}
