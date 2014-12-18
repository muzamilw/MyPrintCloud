
namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Get Items List View Domain Model
    /// </summary>
    public class GetItemsListView
    {
        #region Persisted Properties
        
        /// <summary>
        /// Item Id
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// Item Code
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Estimate Id
        /// </summary>
        public long? EstimateId { get; set; }
        public string ProductName { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string ProductSpecification { get; set; }
        public string CompleteSpecification { get; set; }
        public string ProductCode { get; set; }
        public bool? IsPublished { get; set; }
        public bool? IsEnabled { get; set; }
        public string IconPath { get; set; }
        public bool? IsPopular { get; set; }
        public bool? IsFeatured { get; set; }
        public bool? IsPromotional { get; set; }
        public bool? IsArchived { get; set; }
        public int? SortOrder { get; set; }
        public int? ProductType { get; set; }
        public bool? IsQtyRanged { get; set; }
        public long? OrganisationId { get; set; }
        public string ProductCategoryName { get; set; }
        public double MinPrice { get; set; }
        public bool? IsSpecialItem { get; set; }
        public string WebDescription { get; set; }
        public int? PriceDiscountPercentage { get; set; }
        public bool? IsTemplateDesignMode { get; set; }
        public double? DefaultItemTax { get; set; }
        public bool? IsUploadImage { get; set; }

        #endregion
    }
}
