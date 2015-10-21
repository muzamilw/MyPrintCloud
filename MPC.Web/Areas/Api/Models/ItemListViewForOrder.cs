using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Webapi Model
    /// </summary>
    public class ItemListViewForOrder
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
        public string ProductCode { get; set; }
        public int? SortOrder { get; set; }
        public int? ProductType { get; set; }
        public bool? IsQtyRanged { get; set; }
        public long? OrganisationId { get; set; }
        public string ProductCategoryName { get; set; }
        public long? CompanyId { get; set; }
        public double? DefaultItemTax { get; set; }
        public string JobDescriptionTitle1 { get; set; }
        public string JobDescriptionTitle2 { get; set; }
        public string JobDescriptionTitle3 { get; set; }
        public string JobDescriptionTitle4 { get; set; }
        public string JobDescriptionTitle5 { get; set; }
        public string JobDescriptionTitle6 { get; set; }
        public string JobDescriptionTitle7 { get; set; }
        public string JobDescription1 { get; set; }
        public string JobDescription2 { get; set; }
        public string JobDescription3 { get; set; }
        public string JobDescription4 { get; set; }
        public string JobDescription5 { get; set; }
        public string JobDescription6 { get; set; }
        public string JobDescription7 { get; set; }
        public string CompanyName { get; set; }
        public string ProductSpecification { get; set; }
        public string ThumbnailPath { get; set; }
        public byte[] ThumbnailImage { get; set; }
        /// <summary>
        /// Image Source
        /// </summary>
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