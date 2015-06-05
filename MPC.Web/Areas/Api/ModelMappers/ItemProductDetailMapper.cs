using MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;

    /// <summary>
    /// Item Vdp Price Mapper
    /// </summary>
    public static class ItemProductDetailMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ItemProductDetail CreateFrom(this DomainModels.ItemProductDetail source)
        {
            if (source == null)
            {
                return null;
            }

            return new ItemProductDetail
            {
                ItemDetailId = source.ItemDetailId,
                ItemId = source.ItemId,
                IsAutoCreateSupplierPO = source.isAutoCreateSupplierPO,
                IsInternalActivity = source.isInternalActivity,
                IsQtyLimit = source.isQtyLimit,
                QtyLimit = source.QtyLimit,
                DeliveryTimeSupplier1 = source.DeliveryTimeSupplier1,
                DeliveryTimeSupplier2 = source.DeliveryTimeSupplier2,
                IsPrintItem = source.isPrintItem,
                IsAllowMarketBriefAttachment = source.isAllowMarketBriefAttachment,
                MarketBriefSuccessMessage = source.MarketBriefSuccessMessage
            };
        }

        /// <summary>
        /// Crete From WebApi Model
        /// </summary>
        public static DomainModels.ItemProductDetail CreateFrom(this ItemProductDetail source)
        {
            if (source == null)
            {
                return null;
            }

            return new DomainModels.ItemProductDetail
            {
                ItemDetailId = source.ItemDetailId,
                ItemId = source.ItemId,
                isAutoCreateSupplierPO = source.IsAutoCreateSupplierPO,
                isInternalActivity = source.IsInternalActivity,
                isQtyLimit = source.IsQtyLimit,
                QtyLimit = source.QtyLimit,
                DeliveryTimeSupplier1 = source.DeliveryTimeSupplier1,
                DeliveryTimeSupplier2 = source.DeliveryTimeSupplier2,
                isPrintItem = source.IsPrintItem,
                isAllowMarketBriefAttachment = source.IsAllowMarketBriefAttachment,
                MarketBriefSuccessMessage = source.MarketBriefSuccessMessage
            };
        }

    }
}