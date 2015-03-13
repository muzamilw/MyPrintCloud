using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;


namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Currency Mapper
    /// </summary>
    public static class CurrencyMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static CurrencyDropDown CreateFromDropDown(this DomainModels.Currency source)
        {
            return new CurrencyDropDown
            {
                CurrencyId = source.CurrencyId,
                CurrencyName = source.CurrencyCode+" "+source.CurrencySymbol,
            };
        }

        #endregion
    }
}