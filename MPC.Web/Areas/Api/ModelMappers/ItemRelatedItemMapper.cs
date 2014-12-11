using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Item Vdp Price Mapper
    /// </summary>
    public static class ItemRelatedItemMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemRelatedItem CreateFrom(this DomainModels.ItemRelatedItem source)
        {
            ItemRelatedItem relatedItem = new ItemRelatedItem
            {
                RelatedItemId = source.RelatedItemId,
                ItemId = source.ItemId,
                Id = source.Id
            };

            if (source.RelatedItem == null)
            {
                return relatedItem;
            }

            relatedItem.RelatedItemName = source.RelatedItem.ProductName;
            relatedItem.RelatedItemCode = source.RelatedItem.ProductCode;

            return relatedItem;
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.ItemRelatedItem CreateFrom(this ItemRelatedItem source)
        {
            return new DomainModels.ItemRelatedItem
            {
                RelatedItemId = source.RelatedItemId,
                ItemId = source.ItemId,
                Id = source.Id
            };
        }

    }
}