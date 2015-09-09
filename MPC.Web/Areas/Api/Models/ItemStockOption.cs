using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Stock Option WebApi Model
    /// </summary>
    public class ItemStockOption
    {
        /// <summary>
        /// Unique Id
        /// </summary>
        public long ItemStockOptionId { get; set; }

        /// <summary>
        /// Item Id
        /// </summary>
        public long? ItemId { get; set; }

        /// <summary>
        /// Option Sequence
        /// </summary>
        public int? OptionSequence { get; set; }

        /// <summary>
        /// Stock Id
        /// </summary>
        public long? StockId { get; set; }

        /// <summary>
        /// Stock Label
        /// </summary>
        public string StockLabel { get; set; }

        /// <summary>
        /// Company Id
        /// </summary>
        public long? CompanyId { get; set; }

        /// <summary>
        /// Image Url
        /// </summary>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Stock Item Name
        /// </summary>
        public string StockItemName { get; set; }

        /// <summary>
        /// In Stock
        /// </summary>
        public double? inStock { get; set; }

       
        /// <summary>
        /// Allocated
        /// </summary>
        public double? Allocated { get; set; }
        /// <summary>
        /// Stock Item Description
        /// </summary>
        public string StockItemDescription { get; set; }

        /// <summary>
        /// ImageUrl
        /// </summary>
        public byte[] ImageUrlBytes { get; set; }

        /// <summary>
        /// ImageUrl Source
        /// </summary>
        public string ImageUrlSource
        {
            get
            {
                if (ImageUrlBytes == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(ImageUrlBytes);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }

        /// <summary>
        /// File in Base64
        /// </summary>
        public string FileSource { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; }

        public IEnumerable<ItemAddOnCostCentre> ItemAddOnCostCentres { get; set; }

    }
}