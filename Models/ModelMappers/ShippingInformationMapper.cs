using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Shipping Information mapper
    /// </summary>
    public static class ShippingInformationMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ShippingInformation source, ShippingInformation target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ShippingId = source.ShippingId;
            target.ItemId = source.ItemId;
            target.AddressId = source.AddressId;
            target.EstimateId = source.EstimateId;
            target.Quantity = source.Quantity;
            target.Price = source.Price;
            target.DeliveryCost = source.DeliveryCost;
            target.DeliveryDate = source.DeliveryDate;
            target.DeliveryNoteRaised = source.DeliveryNoteRaised;
            target.CarrierId = source.CarrierId;
            target.ConsignmentNumber = source.ConsignmentNumber;
        }

        #endregion
    }
}
