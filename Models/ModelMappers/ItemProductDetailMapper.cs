using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item ProductDetail mapper
    /// </summary>
    public static class ItemProductDetailMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemProductDetail source, ItemProductDetail target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ItemDetailId = source.ItemDetailId;
            target.ItemId = source.ItemId;
            target.isInternalActivity = source.isInternalActivity;
            target.isQtyLimit = source.isQtyLimit;
            target.isAutoCreateSupplierPO = source.isAutoCreateSupplierPO;
            target.QtyLimit = source.QtyLimit;
            target.DeliveryTimeSupplier1 = source.DeliveryTimeSupplier1;
            target.DeliveryTimeSupplier2 = source.DeliveryTimeSupplier2;
            target.isPrintItem = source.isPrintItem;
            target.isAllowMarketBriefAttachment = source.isAllowMarketBriefAttachment;
            target.MarketBriefSuccessMessage = source.MarketBriefSuccessMessage;
        }

        #endregion
    }
}
