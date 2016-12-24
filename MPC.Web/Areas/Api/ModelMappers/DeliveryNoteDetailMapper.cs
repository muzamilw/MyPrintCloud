using MPC.MIS.Areas.Api.Models;
using DomainModel = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Delivery Note Detail API Mapper
    /// </summary>
    public static class DeliveryNoteDetailMapper
    {
        /// <summary>
        /// Create Form Domain Model
        /// </summary>
        public static DeliveryNoteDetail CreateFrom(this DomainModel.DeliveryNoteDetail source)
        {
            return new DeliveryNoteDetail
            {
                DeliveryDetailid = source.DeliveryDetailid,
                Description = source.Description,
                ItemQty = source.ItemQty,
                ItemId = source.ItemId,
                GrossItemTotal = source.GrossItemTotal
            };
        }


        /// <summary>
        /// Create Form Api Model
        /// </summary>
        public static DomainModel.DeliveryNoteDetail CreateFrom(this DeliveryNoteDetail source)
        {
            return new DomainModel.DeliveryNoteDetail
            {
                DeliveryDetailid = source.DeliveryDetailid,
                Description = source.Description,
                ItemQty = source.ItemQty,
                ItemId = source.ItemId,
                GrossItemTotal = source.GrossItemTotal
            };
        }
    }
}