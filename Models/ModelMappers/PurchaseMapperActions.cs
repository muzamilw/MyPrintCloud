using System;
using MPC.Models.DomainModels;

namespace MPC.Models.ModelMappers
{
    /// <summary>
    /// Purchase Mapper Actions
    /// </summary>
    public sealed class PurchaseMapperActions
    {
        /// <summary>
        /// Action to create a Purchase Detail
        /// </summary>
        public Func<PurchaseDetail> CreatePurchaseDetail { get; set; }

        /// <summary>
        /// Action to delete a Purchase Detail
        /// </summary>
        public Action<PurchaseDetail> DeletePurchaseDetail { get; set; }

    }
}
