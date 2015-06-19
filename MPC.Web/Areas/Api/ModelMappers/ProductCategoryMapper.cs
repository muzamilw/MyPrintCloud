using System.IO;
using System.Linq;
using System.Web;
using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ProductCategoryMapper
    {
        public static ProductCategory CreateFrom(this DomainModels.ProductCategory source)
        {
            byte[] thumbnailPathBytes = null;
            string path = null;
            if (!string.IsNullOrEmpty(source.ThumbnailPath))
            {
                path = HttpContext.Current.Server.MapPath("~/" + source.ThumbnailPath);
                if (File.Exists(path))
                {
                    thumbnailPathBytes = File.ReadAllBytes(path);
                } 
            }
            byte[] imagePathBytes = null;
            if (!string.IsNullOrEmpty(source.ImagePath))
            {
                path = HttpContext.Current.Server.MapPath("~/" + source.ImagePath);
                if (File.Exists(path))
                {
                    imagePathBytes = File.ReadAllBytes(path);
                } 
                //imagePathBytes = source.ImagePath != null ? File.ReadAllBytes(source.ImagePath) : null;
            }
            var productCategory = new ProductCategory
            {
                ProductCategoryId = source.ProductCategoryId,
                CategoryName = source.CategoryName,
                ContentType = source.ContentType,
                Description1 = source.Description1,
                Description2 = source.Description2,
                LockedBy = source.LockedBy,
                CompanyId = source.CompanyId,
                ParentCategoryId = source.ParentCategoryId,
                DisplayOrder = source.DisplayOrder,
                ImagePath = source.ImagePath,
                ThumbnailPath = path ?? string.Empty,
                isEnabled = source.isEnabled,
                isMarketPlace = source.isMarketPlace,
                TemplateDesignerMappedCategoryName = source.TemplateDesignerMappedCategoryName,
                isArchived = source.isArchived,
                isPublished = source.isPublished,
                TrimmedWidth = source.TrimmedWidth,
                TrimmedHeight = source.TrimmedHeight,
                isColorImposition = source.isColorImposition,
                isOrderImposition = source.isOrderImposition,
                isLinkToTemplates = source.isLinkToTemplates,
                Sides = source.Sides,
                ApplySizeRestrictions = source.ApplySizeRestrictions,
                ApplyFoldLines = source.ApplyFoldLines,
                WidthRestriction = source.WidthRestriction,
                HeightRestriction = source.HeightRestriction,
                CategoryTypeId = source.CategoryTypeId,
                RegionId = source.RegionId,
                ZoomFactor = source.ZoomFactor,
                ScaleFactor = source.ScaleFactor,
                isShelfProductCategory = source.isShelfProductCategory,
                MetaKeywords = source.MetaKeywords,
                MetaDescription = source.MetaDescription,
                MetaTitle = source.MetaTitle,
                OrganisationId = source.OrganisationId,
                SubCategoryDisplayMode1 = source.SubCategoryDisplayMode1,
                SubCategoryDisplayMode2 = source.SubCategoryDisplayMode2,
                SubCategoryDisplayColumns = source.SubCategoryDisplayColumns,
                CategoryURLText = source.CategoryURLText,
                MetaOverride = source.MetaOverride,
                ShortDescription = source.ShortDescription,
                SecondaryDescription = source.SecondaryDescription,
                DefaultSortBy = source.DefaultSortBy,
                ProductsDisplayColumns = source.ProductsDisplayColumns,
                ProductsDisplayRows = source.ProductsDisplayRows,
                IsDisplayFeaturedproducts = source.IsDisplayFeaturedproducts,
                IsShowAvailablity = source.IsShowAvailablity,
                IsShowRewardPoints = source.IsShowRewardPoints,
                IsShowListPrice = source.IsShowListPrice,
                IsShowSalePrice = source.IsShowSalePrice,
                IsShowStockStatus = source.IsShowStockStatus,
                IsShowProductDescription = source.IsShowProductDescription,
                IsShowProductShortDescription = source.IsShowProductShortDescription,
                //ThumbnailStreamId = source.ThumbnailStreamId,
                //ImageStreamId = source.ImageStreamId,
                Image = imagePathBytes,
                ThumbNail = thumbnailPathBytes,
                CategoryTerritories = source.CategoryTerritories != null ? source.CategoryTerritories.Select(x=> x.CreateFromCategoryTerritory()).ToList(): null
            };

            return productCategory;
        }

        public static DomainModels.ProductCategory CreateFrom(this ProductCategory source)
        {
            return new DomainModels.ProductCategory
            {
                ProductCategoryId = source.ProductCategoryId,
                CategoryName = source.CategoryName,
                ContentType = source.ContentType,
                Description1 = source.Description1,
                Description2 = source.Description2,
                LockedBy = source.LockedBy,
                CompanyId = source.CompanyId,
                ParentCategoryId = source.ParentCategoryId,
                DisplayOrder = source.DisplayOrder,
                isEnabled = source.isEnabled ?? false,
                isMarketPlace = source.isMarketPlace,
                TemplateDesignerMappedCategoryName = source.TemplateDesignerMappedCategoryName,
                isArchived = source.isArchived == null,
                isPublished = source.isPublished ?? false,
                TrimmedWidth = source.TrimmedWidth,
                TrimmedHeight = source.TrimmedHeight,
                isColorImposition = source.isColorImposition,
                isOrderImposition = source.isOrderImposition,
                isLinkToTemplates = source.isLinkToTemplates,
                Sides = source.Sides,
                ApplySizeRestrictions = source.ApplySizeRestrictions,
                ApplyFoldLines = source.ApplyFoldLines,
                WidthRestriction = source.WidthRestriction,
                HeightRestriction = source.HeightRestriction,
                CategoryTypeId = source.CategoryTypeId,
                RegionId = source.RegionId,
                ZoomFactor = source.ZoomFactor,
                ScaleFactor = source.ScaleFactor,
                isShelfProductCategory = source.isShelfProductCategory,
                MetaKeywords = source.MetaKeywords,
                MetaDescription = source.MetaDescription,
                MetaTitle = source.MetaTitle,
                SubCategoryDisplayMode1 = source.SubCategoryDisplayMode1,
                SubCategoryDisplayMode2 = source.SubCategoryDisplayMode2,
                SubCategoryDisplayColumns = source.SubCategoryDisplayColumns,
                CategoryURLText = source.CategoryURLText,
                MetaOverride = source.MetaOverride,
                ShortDescription = source.ShortDescription,
                SecondaryDescription = source.SecondaryDescription,
                DefaultSortBy = source.DefaultSortBy,
                ProductsDisplayColumns = source.ProductsDisplayColumns,
                ProductsDisplayRows = source.ProductsDisplayRows,
                IsDisplayFeaturedproducts = source.IsDisplayFeaturedproducts,
                IsShowAvailablity = source.IsShowAvailablity,
                IsShowRewardPoints = source.IsShowRewardPoints,
                IsShowListPrice = source.IsShowListPrice,
                IsShowSalePrice = source.IsShowSalePrice,
                IsShowStockStatus = source.IsShowStockStatus,
                IsShowProductDescription = source.IsShowProductDescription,
                IsShowProductShortDescription = source.IsShowProductShortDescription,
                ImageBytes = source.ImageBytes,
                ThumbNailBytes = source.ThumbnailBytes,
                CategoryTerritories = source.CategoryTerritories != null ? source.CategoryTerritories.Select(x => x.CreateFromTerritory()).ToList() : null
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ProductCategoryDropDown CreateFromDropDown(this DomainModels.ProductCategory source)
        {
            return new ProductCategoryDropDown
            {
                ProductCategoryId = source.ProductCategoryId,
                CategoryName = source.CategoryName,
                CategoryTypeId = source.CategoryTypeId,
                IsArchived = source.isArchived == true
            };
        }

        /// <summary>
        /// Crete From For Template Properties
        /// </summary>
        public static ProductCategoryForTemplate CreateFromForTemplate(this DomainModels.ProductCategory source)
        {
            return new ProductCategoryForTemplate
            {
                ProductCategoryId = source.ProductCategoryId,
                CategoryName = source.CategoryName,
                CategoryTypeId = source.CategoryTypeId,
                RegionId = source.RegionId,
                ZoomFactor = source.ZoomFactor,
                ScaleFactor = source.ScaleFactor
            };
        }

    }
}
