using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class DeliveryCarrierMapper
    {

        public static DomainModels.DeliveryCarrier CreateFrom(this DeliveryCarrier source)
        {
            return new DomainModels.DeliveryCarrier
            {
                CarrierId = source.CarrierId,
                CarrierName = source.CarrierName,
                Url = source.Url,
                ApiKey = source.ApiKey,
                ApiPassword = source.ApiPassword,
                isEnable = source.isEnable,
                CarrierPhone = source.CarrierPhone
            };
        }

        public static DeliveryCarrier CreateFrom(this DomainModels.DeliveryCarrier source)
        {
            return new DeliveryCarrier
            {
                CarrierId = source.CarrierId,
                CarrierName = source.CarrierName,
                Url = source.Url,
                ApiKey = source.ApiKey,
                ApiPassword = source.ApiPassword,
                isEnable = source.isEnable,
                CarrierPhone = source.CarrierPhone

            };
        }
        public static DeliveryCarrier CreateFromDropDown(this DomainModels.DeliveryCarrier source)
        {
            return new DeliveryCarrier
            {
                CarrierId = source.CarrierId,
                CarrierName = source.CarrierName,
                CarrierPhone = source.CarrierPhone,
                Url = source.Url
            };
        }
    }
}