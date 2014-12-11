using System.Collections.Generic;
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
            return new Item
            {
                ItemId = source.ItemId,
                ItemCode = source.ItemCode,
                ProductCode = source.ProductCode,
                ProductName = source.ProductName,
                ProductSpecification = source.ProductSpecification,
                GridImage = source.GridImage,
                ThumbnailPath = source.ThumbnailPath,
                ProductCategoryId = source.ProductCategoryId,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                OrganisationId = source.OrganisationId,
                IsFinishedGoods = source.IsFinishedGoods,
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
                ItemVdpPrices = source.ItemVdpPrices != null ? source.ItemVdpPrices.Select(vdp => vdp.CreateFrom()) : new List<ItemVdpPrice>(),
                ItemVideos = source.ItemVideos != null ? source.ItemVideos.Select(vdp => vdp.CreateFrom()) : new List<ItemVideo>(),
                ItemRelatedItems = source.ItemRelatedItems != null ? source.ItemRelatedItems.Select(vdp => vdp.CreateFrom()) : new List<ItemRelatedItem>()
            };
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
                ProductCategoryId = source.ParentCategoryId,
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
                ProductCategoryId = source.ProductCategoryId,
                IsArchived = source.IsArchived,
                IsEnabled = source.IsEnabled,
                IsPublished = source.IsPublished,
                OrganisationId = source.OrganisationId,
                IsFinishedGoods = source.IsFinishedGoods,
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
                ItemVdpPrices = source.ItemVdpPrices != null ? source.ItemVdpPrices.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.ItemVdpPrice>(),
                ItemVideos = source.ItemVideos != null ? source.ItemVideos.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.ItemVideo>(),
                ItemRelatedItems = source.ItemRelatedItems != null ? source.ItemRelatedItems.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.ItemRelatedItem>()
            };
        }
    }
}