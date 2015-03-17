using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Webapi Model
    /// </summary>
    public class ItemListView
    {
        #region Public

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
        public int? IsSpecialItem { get; set; }
        public long? CompanyId { get; set; }
        public long? TemplateId { get; set; }
        public bool? PrintCropMarks { get; set; }
        public bool? DrawWaterMarkTxt { get; set; }
        public int? TemplateType { get; set; }
        public byte[] ThumbnailImage { get; set; }
        public string ThumbnailImageSource
        {
            get
            {
                if (ThumbnailImage == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(ThumbnailImage);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }

        #endregion
    }
}