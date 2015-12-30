using System;
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Get Category Product Domain Model
    /// </summary>
    public class GetCategoryProduct
    {
        public Nullable<long> CompanyId { get; set; }
        public long ItemId { get; set; }
        public string ItemCode { get; set; }
        public Nullable<bool> isQtyRanged { get; set; }
        public Nullable<long> EstimateId { get; set; }
        public string ProductName { get; set; }
        public string ProductCode { get; set; }
        public string ProductCategoryName { get; set; }
        public double MinPrice { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string IconPath { get; set; }
        public Nullable<bool> IsEnabled { get; set; }
        public Nullable<bool> IsSpecialItem { get; set; }
        public Nullable<bool> IsPopular { get; set; }
        public Nullable<bool> IsFeatured { get; set; }
        public Nullable<bool> IsPromotional { get; set; }
        public Nullable<bool> IsPublished { get; set; }
        public Nullable<int> ProductType { get; set; }
        public string ProductSpecification { get; set; }
        public string CompleteSpecification { get; set; }
        public Nullable<bool> IsArchived { get; set; }
        public Nullable<int> SortOrder { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public string WebDescription { get; set; }
        public Nullable<int> PriceDiscountPercentage { get; set; }
        public Nullable<int> isTemplateDesignMode { get; set; }
        public Nullable<double> DefaultItemTax { get; set; }
        public Nullable<bool> isUploadImage { get; set; }
        public Nullable<bool> isMarketingBrief { get; set; }
        public long ProductCategoryId { get; set; }
        public Nullable<long> TemplateId { get; set; }
        public Nullable<int> DesignerCategoryId { get; set; }
        public string ItemFriendlyName { get; set; }

        public float PDFTemplateHeight { get; set; }
        public float PDFTemplateWidth { get; set; }
        public Nullable<bool> IsAllowCustomSize { get; set; }
    }
}
