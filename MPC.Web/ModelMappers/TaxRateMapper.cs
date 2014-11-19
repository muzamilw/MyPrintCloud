using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.Web.Models;

namespace MPC.Web.ModelMappers
{
    /// <summary>
    /// Tax Rate Mapper
    /// </summary>
    public static class TaxRateMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.TaxRate CreateFrom(this DomainModels.TaxRate source)
        {
            return new ApiModels.TaxRate
            {
                TaxId = source.TaxId,
                TaxName = source.TaxName,
                Tax1 = source.Tax1,
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.TaxRate CreateFrom(this ApiModels.TaxRate source)
        {
            return new DomainModels.TaxRate
            {
                TaxId = source.TaxId,
                TaxName = source.TaxName,
                Tax1 = source.Tax1,
            };
        }
        #endregion
    }
}