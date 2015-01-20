using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Cms Offer Mapper
    /// </summary>
    public static class CmsOfferMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CmsOffer CreateFrom(this DomainModels.CmsOffer source)
        {
            return new CmsOffer
            {
                OfferId = source.OfferId,
                ItemId = source.ItemId,
                ItemName = source.ItemName,
                OfferType = source.OfferType,
                SortOrder = source.SortOrder
            };
        }

        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static DomainModels.CmsOffer CreateFrom(this CmsOffer source)
        {
            return new DomainModels.CmsOffer
            {
                OfferId = source.OfferId,
                ItemId = source.ItemId,
                ItemName = source.ItemName,
                OfferType = source.OfferType,
                SortOrder = source.SortOrder
            };
        }
        #endregion
    }
}