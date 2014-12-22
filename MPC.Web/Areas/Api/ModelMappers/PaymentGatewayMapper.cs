using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class PaymentGatewayMapper
    {
        public static PaymentGateway CreateFrom(this DomainModels.PaymentGateway source)
        {
            return new PaymentGateway
            {
                PaymentGatewayId = source.PaymentGatewayId,
                BusinessEmail = source.BusinessEmail,
                IdentityToken = source.IdentityToken,
                IsActive = source.isActive,
                CompanyId = source.CompanyId,
                PaymentMethodId = source.PaymentMethodId,
                SecureHash = source.SecureHash
            };
        }

        public static DomainModels.PaymentGateway CreateFrom(this PaymentGateway source)
        {
            return new DomainModels.PaymentGateway
            {
                PaymentGatewayId = source.PaymentGatewayId,
                BusinessEmail = source.BusinessEmail,
                IdentityToken = source.IdentityToken,
                isActive = source.IsActive,
                CompanyId = source.CompanyId,
                PaymentMethodId = source.PaymentMethodId,
                SecureHash = source.SecureHash
            };
        }
    }
}