using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class PaymentMethodMapper
    {//PaymentMethodId  MethodName  IsActive
        public static PaymentMethod CreateFrom(this DomainModels.PaymentMethod source)
        {
            return new PaymentMethod
            {
                PaymentMethodId = source.PaymentMethodId,
                MethodName = source.MethodName,
                IsActive = source.IsActive
            };
        }

        public static DomainModels.PaymentMethod CreateFrom(this PaymentMethod source)
        {
            return new DomainModels.PaymentMethod
            {
                PaymentMethodId = source.PaymentMethodId,
                MethodName = source.MethodName,
                IsActive = source.IsActive
            };
        }
    }
}