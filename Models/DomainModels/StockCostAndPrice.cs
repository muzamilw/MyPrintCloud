using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Stock Cost And Price Domain Model
    /// </summary>
    public class StockCostAndPrice
    {
        #region Persisted Properties
        /// <summary>
        /// Cost Price Id
        /// </summary>
        public int CostPriceId { get; set; }

        /// <summary>
        /// Stick Item Id
        /// </summary>
        public int ItemId { get; set; }

        /// <summary>
        /// Cost Price
        /// </summary>
        public float CostPrice { get; set; }

        /// <summary>
        /// Pack Cost Price
        /// </summary>
        public float PackCostPrice { get; set; }

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

        /// <summary>
        /// Processing Charge
        /// </summary>
        public float ProcessingCharge { get; set; }
        #endregion

        #region Reference Properties
        /// <summary>
        /// Stock Item
        /// </summary>
        public virtual StockItem StockItem { get; set; }
        #endregion
    }
}
