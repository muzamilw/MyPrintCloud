﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Product Category
    /// </summary>
    public class ProductCategory
    {
        public long ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ContentType { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public int LockedBy { get; set; }
        public long? CompanyId { get; set; }
        public int? ParentCategoryId { get; set; }
        public int DisplayOrder { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }
        public bool? isEnabled { get; set; }
        public bool? isMarketPlace { get; set; }
        public string TemplateDesignerMappedCategoryName { get; set; }
        public bool? isArchived { get; set; }
        public bool? isPublished { get; set; }
        public double? TrimmedWidth { get; set; }
        public double? TrimmedHeight { get; set; }
        public bool? isColorImposition { get; set; }
        public bool? isOrderImposition { get; set; }
        public bool? isLinkToTemplates { get; set; }
        public int? Sides { get; set; }
        public bool? ApplySizeRestrictions { get; set; }
        public bool? ApplyFoldLines { get; set; }
        public double? WidthRestriction { get; set; }
        public double? HeightRestriction { get; set; }
        public int? CategoryTypeId { get; set; }
        public int? RegionId { get; set; }
        public decimal? ZoomFactor { get; set; }
        public decimal? ScaleFactor { get; set; }
        public bool? isShelfProductCategory { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public long? OrganisationId { get; set; }
        public int? SubCategoryDisplayMode1 { get; set; }
        public int? SubCategoryDisplayMode2 { get; set; }
        public int? SubCategoryDisplayColumns { get; set; }
        public string CategoryURLText { get; set; }
        public string MetaOverride { get; set; }
        public string ShortDescription { get; set; }
        public string SecondaryDescription { get; set; }
        public int? DefaultSortBy { get; set; }
        public int? ProductsDisplayColumns { get; set; }
        public int? ProductsDisplayRows { get; set; }
        public bool? IsDisplayFeaturedproducts { get; set; }
        public bool? IsShowAvailablity { get; set; }
        public bool? IsShowRewardPoints { get; set; }
        public bool? IsShowListPrice { get; set; }
        public bool? IsShowSalePrice { get; set; }
        public bool? IsShowStockStatus { get; set; }
        public bool? IsShowProductDescription { get; set; }
        public bool? IsShowProductShortDescription { get; set; }
        
        public virtual Company Company { get; set; }
        public virtual ICollection<ProductCategoryItem> ProductCategoryItems { get; set; }
        public virtual ICollection<CategoryTerritory> CategoryTerritories { get; set; }

        #region Additional Properties
        /// <summary>
        /// Product Category ThumbNail File Bytes
        /// </summary>
        [NotMapped]
       public string ThumbNailBytes { get; set; }

        
        /// <summary>
        /// Product Category Image File Bytes
        /// </summary>
        [NotMapped]
        public string ImageBytes { get; set; }

        #endregion
    }
}
