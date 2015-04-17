using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Pre Payment mapper
    /// </summary>
    public static class PrePaymentMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this PrePayment source, PrePayment target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.PrePaymentId = source.PrePaymentId;
            target.CustomerId = source.CustomerId;
            target.PaymentDate = source.PaymentDate;
            target.ReferenceCode = source.ReferenceCode;
            target.Amount = source.Amount;
            target.PaymentMethodId = source.PaymentMethodId;
            target.PaymentDescription = source.PaymentDescription;
        }

        #endregion
    }
}
