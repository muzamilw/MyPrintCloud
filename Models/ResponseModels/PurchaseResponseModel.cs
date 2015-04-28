using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class PurchaseResponseModel
    {
        /// <summary>
        /// Purchases
        /// </summary>
        public IEnumerable<Purchase> Purchases { get; set; }

        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
