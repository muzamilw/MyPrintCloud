using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class OrderRetailItemDetail
    {
        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<ItemStockOption> ItemStockOptions { get; set; }

        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<ItemPriceMatrix> ItemPriceMatrices { get; set; }

    }
}