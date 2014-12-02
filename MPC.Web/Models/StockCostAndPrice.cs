using System;

namespace MPC.MIS.Models
{
    /// <summary>
    /// Stock Cost And Price Web Model
    /// </summary>
    public class StockCostAndPrice
    {
        /// <summary>
        /// Cost Price Id
        /// </summary>
        public int CostPriceId { get; set; }

        /// <summary>
        /// Cost Price
        /// </summary>
        public double CostPrice { get; set; }

        /// <summary>
        /// Pack Cost Price
        /// </summary>
        public double PackCostPrice { get; set; }

        /// <summary>
        /// From Date
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// To Date
        /// </summary>
        public DateTime? ToDate { get; set; }

        /// <summary>
        /// Cost Or Price Identifier
        /// </summary>
        public short CostOrPriceIdentifier { get; set; }
    }
}