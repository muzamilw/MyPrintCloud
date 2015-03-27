using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Pre Payment Mapper
    /// </summary>
    public static class PrePaymentMapper
    {
        #region Public
        /// <summary>
        /// Create From Web Model
        /// </summary>
        public static DomainModels.PrePayment CreateFrom(this PrePayment source)
        {
            return new DomainModels.PrePayment
            {
                PaymentMethodId = source.PaymentMethodId,
                CustomerId = source.CustomerId,
                Amount = source.Amount,
                OrderId = source.OrderId,
                PaymentDate = source.PaymentDate,
                PaymentDescription = source.PaymentDescription,
                PrePaymentId = source.PrePaymentId,
                ReferenceCode = source.ReferenceCode,
            };
        }

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static PrePayment CreateFrom(this DomainModels.PrePayment source)
        {
            return new PrePayment
            {
                PaymentMethodId = source.PaymentMethodId,
                CustomerId = source.CustomerId,
                Amount = source.Amount,
                OrderId = source.OrderId,
                PaymentDate = source.PaymentDate,
                PaymentDescription = source.PaymentDescription,
                PrePaymentId = source.PrePaymentId,
                ReferenceCode = source.ReferenceCode,
                PaymentMethodName = source.PaymentMethod != null ? source.PaymentMethod.MethodName : string.Empty,
            };
        }


        #endregion
    }
}