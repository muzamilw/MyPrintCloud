using System.Collections.Generic;
using System.IO;
using System.Linq;
using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Stock Item Mapper
    /// </summary>
    public static class ItemMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Item CreateFrom(this DomainModels.Item source)
        {
            Item item = new Item
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                GridImage = source.GridImage,
                ThumbnailPath = source.ThumbnailPath,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                OrganisationId = source.OrganisationId,
                ProductType = source.ProductType,
                SortOrder = source.SortOrder,
                IsFeatured = source.IsFeatured,
                IsVdpProduct = source.IsVdpProduct,
                IsStockControl = source.IsStockControl,
                WebDescription = source.WebDescription,
                TipsAndHints = source.TipsAndHints,
                XeroAccessCode = source.XeroAccessCode,
                MetaTitle = source.MetaTitle,
                MetaDescription = source.MetaDescription,
                MetaKeywords = source.MetaKeywords,
                JobDescriptionTitle1 = source.JobDescriptionTitle1,
                JobDescription1 = source.JobDescription1,
                JobDescriptionTitle2 = source.JobDescriptionTitle2,
                JobDescription2 = source.JobDescription2,
                JobDescriptionTitle3 = source.JobDescriptionTitle3,
                JobDescription3 = source.JobDescription3,
                JobDescriptionTitle4 = source.JobDescriptionTitle4,
                JobDescription4 = source.JobDescription4,
                JobDescriptionTitle5 = source.JobDescriptionTitle5,
                JobDescription5 = source.JobDescription5,
                JobDescriptionTitle6 = source.JobDescriptionTitle6,
                JobDescription6 = source.JobDescription6,
                JobDescriptionTitle7 = source.JobDescriptionTitle7,
                JobDescription7 = source.JobDescription7,
                JobDescriptionTitle8 = source.JobDescriptionTitle8,
                JobDescription8 = source.JobDescription8,
                JobDescriptionTitle9 = source.JobDescriptionTitle9,
                JobDescription9 = source.JobDescription9,
                JobDescriptionTitle10 = source.JobDescriptionTitle10,
                JobDescription10 = source.JobDescription10,
                TemplateId = source.TemplateId,
                FlagId = source.FlagId,
                IsQtyRanged = source.IsQtyRanged,
                PackagingWeight = source.PackagingWeight,
                DefaultItemTax = source.DefaultItemTax,
                SupplierId = source.SupplierId,
                SupplierId2 = source.SupplierId2,
                EstimateProductionTime = source.EstimateProductionTime,
                ItemProductDetail = source.ItemProductDetails != null && source.ItemProductDetails.Count > 0 ?
                source.ItemProductDetails.FirstOrDefault().CreateFrom() : null,
                Template = source.Template != null ? source.Template.CreateFrom() : new Template(),
                ItemVdpPrices = source.ItemVdpPrices != null ? source.ItemVdpPrices.Select(vdp => vdp.CreateFrom()) : new List<ItemVdpPrice>(),
                ItemVideos = source.ItemVideos != null ? source.ItemVideos.Select(vdp => vdp.CreateFrom()) : new List<ItemVideo>(),
                ItemRelatedItems = source.ItemRelatedItems != null ? source.ItemRelatedItems.Select(vdp => vdp.CreateFrom()) : new List<ItemRelatedItem>(),
                ItemStockOptions = source.ItemStockOptions != null ? source.ItemStockOptions.Select(vdp => vdp.CreateFrom()) : new List<ItemStockOption>(),
                ItemStateTaxes = source.ItemStateTaxes != null ? source.ItemStateTaxes.Select(vdp => vdp.CreateFrom()) : new List<ItemStateTax>(),
                ItemPriceMatrices = source.ItemPriceMatrices != null ? source.ItemPriceMatrices.Select(vdp => vdp.CreateFrom()) : new List<ItemPriceMatrix>()
            };

            // Load Thumbnail Image
            if (source.ThumbnailPath != null && File.Exists(source.ThumbnailPath))
            {
                item.ThumbnailImage = source.ThumbnailPath != null ? File.ReadAllBytes(source.ThumbnailPath) : null;
            }

            // Load Grid Image
            if (source.GridImage != null && File.Exists(source.GridImage))
            {
                item.GridImageBytes = source.GridImage != null ? File.ReadAllBytes(source.GridImage) : null;
            }

            // Load Image Path
            if (source.ImagePath != null && File.Exists(source.ImagePath))
            {
                item.ImagePathImage = source.ImagePath != null ? File.ReadAllBytes(source.ImagePath) : null;
            }

            // Load File1
            if (source.File1 != null && File.Exists(source.File1))
            {
                item.File1Bytes = source.File1 != null ? File.ReadAllBytes(source.File1) : null;
            }

            return item;
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemListView CreateFromForListView(this DomainModels.GetItemsListView source)
        {
            return new ItemListView
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                ThumbnailPath = source.ThumbnailPath,
                ProductCategoryName = source.ProductCategoryName,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                MinPrice = source.MinPrice
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.Item CreateFrom(this Item source)
        {
            return new DomainModels.Item
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                GridImage = source.GridImage,
                ThumbnailPath = source.ThumbnailPath,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                OrganisationId = source.OrganisationId,
                ProductType = source.ProductType,
                SortOrder = source.SortOrder,
                IsFeatured = source.IsFeatured,
                IsVdpProduct = source.IsVdpProduct,
                IsStockControl = source.IsStockControl,
                WebDescription = source.WebDescription,
                TipsAndHints = source.TipsAndHints,
                XeroAccessCode = source.XeroAccessCode,
                MetaTitle = source.MetaTitle,
                MetaDescription = source.MetaDescription,
                MetaKeywords = source.MetaKeywords,
                JobDescriptionTitle1 = source.JobDescriptionTitle1,
                JobDescription1 = source.JobDescription1,
                JobDescriptionTitle2 = source.JobDescriptionTitle2,
                JobDescription2 = source.JobDescription2,
                JobDescriptionTitle3 = source.JobDescriptionTitle3,
                JobDescription3 = source.JobDescription3,
                JobDescriptionTitle4 = source.JobDescriptionTitle4,
                JobDescription4 = source.JobDescription4,
                JobDescriptionTitle5 = source.JobDescriptionTitle5,
                JobDescription5 = source.JobDescription5,
                JobDescriptionTitle6 = source.JobDescriptionTitle6,
                JobDescription6 = source.JobDescription6,
                JobDescriptionTitle7 = source.JobDescriptionTitle7,
                JobDescription7 = source.JobDescription7,
                JobDescriptionTitle8 = source.JobDescriptionTitle8,
                JobDescription8 = source.JobDescription8,
                JobDescriptionTitle9 = source.JobDescriptionTitle9,
                JobDescription9 = source.JobDescription9,
                JobDescriptionTitle10 = source.JobDescriptionTitle10,
                JobDescription10 = source.JobDescription10,
                TemplateId = source.TemplateId,
                FlagId = source.FlagId,
                IsQtyRanged = source.IsQtyRanged,
                PackagingWeight = source.PackagingWeight,
                DefaultItemTax = source.DefaultItemTax,
                SupplierId = source.SupplierId,
                SupplierId2 = source.SupplierId2,
                EstimateProductionTime = source.EstimateProductionTime,
                Template = source.Template != null ? source.Template.CreateFrom() : new DomainModels.Template(),
                ItemVdpPrices = source.ItemVdpPrices != null ? source.ItemVdpPrices.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.ItemVdpPrice>(),
                ItemVideos = source.ItemVideos != null ? source.ItemVideos.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.ItemVideo>(),
                ItemRelatedItems = source.ItemRelatedItems != null ? source.ItemRelatedItems.Select(vdp => vdp.CreateFrom()).ToList() :
                new List<DomainModels.ItemRelatedItem>(),
                ItemStockOptions = source.ItemStockOptions != null ? source.ItemStockOptions.Select(stockOption => stockOption.CreateFrom()).ToList() :
                new List<DomainModels.ItemStockOption>(),
                ItemStateTaxes = source.ItemStateTaxes != null ? source.ItemStateTaxes.Select(vdp => vdp.CreateFrom()).ToList() :
                new List<DomainModels.ItemStateTax>(),
                ItemPriceMatrices = source.ItemPriceMatrices != null ? source.ItemPriceMatrices.Select(vdp => vdp.CreateFrom()).ToList() :
                new List<DomainModels.ItemPriceMatrix>(),
                ItemProductDetails = source.ItemProductDetail != null ? new List<DomainModels.ItemProductDetail> { source.ItemProductDetail.CreateFrom() } :
                new List<DomainModels.ItemProductDetail>(),
                ThumbnailImageName = source.ThumbnailImageName,
                ImagePathImageName = source.ImagePathImageName,
                GridImageSourceName = source.GridImageSourceName,
                ThumbnailImage = source.ThumbnailImage,
                GridImageBytes = source.GridImageBytes,
                ImagePathImage = source.ImagePathImage
            };
        }
    }
}