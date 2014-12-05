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
                ItemVdpPrices = source.ItemVdpPrices != null ? source.ItemVdpPrices.Select(vdp => vdp.CreateFrom()) : new List<ItemVdpPrice>()
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
                ItemVdpPrices = source.ItemVdpPrices != null ? source.ItemVdpPrices.Select(vdp => vdp.CreateFrom()).ToList() : new List<DomainModels.ItemVdpPrice>()
            };
        }
    }
}