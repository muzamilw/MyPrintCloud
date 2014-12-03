using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
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
        public static TaxRate CreateFrom(this DomainModels.TaxRate source)
        {
            return new TaxRate
            {
                TaxId = source.TaxId,
                TaxName = source.TaxName,
                Tax1 = source.Tax1,
            };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.TaxRate CreateFrom(this TaxRate source)
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