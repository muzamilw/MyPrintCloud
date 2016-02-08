using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Shipping Information Mapper
    /// </summary>
    public static class ShippingInformationMapper
    {
        #region Public

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ShippingInformation CreateFrom(this MPC.Models.DomainModels.ShippingInformation source)
        {
            return new ShippingInformation
            {
                ShippingId = source.ShippingId,
                ItemId = source.ItemId,
                Quantity = source.Quantity,
                DeliveryDate = source.DeliveryDate,
                Price = source.Price,
                AddressId = source.AddressId,
                EstimateId = source.EstimateId,
                DeliveryNoteRaised = source.DeliveryNoteRaised,
                CarrierId = source.CarrierId,
                ConsignmentNumber = source.ConsignmentNumber,
                ItemName = source.Item != null ? source.Item.ProductName : string.Empty,
                AddressName = source.Address != null ? source.Address.AddressName : string.Empty
            };
        }

        /// <summary>
        /// Create From WebApi Model
        /// </summary>
        public static MPC.Models.DomainModels.ShippingInformation CreateFrom(this ShippingInformation source)
        {
            return new MPC.Models.DomainModels.ShippingInformation
            {
                ShippingId = source.ShippingId,
                ItemId = source.ItemId,
                Quantity = source.Quantity,
                DeliveryDate = source.DeliveryDate,
                Price = source.Price,
                AddressId = source.AddressId,
                EstimateId = source.EstimateId,
                DeliveryNoteRaised = source.DeliveryNoteRaised,
                CarrierId = source.CarrierId,
                ConsignmentNumber = source.ConsignmentNumber
            };
        }

        #endregion
    }
}