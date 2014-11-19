using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.Web.Models;

namespace MPC.Web.ModelMappers
{
    /// <summary>
    /// Chart Of Account Mapper
    /// </summary>
    public static class ChartOfAccountMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Models.ChartOfAccount CreateFrom(this DomainModels.ChartOfAccount source)
        {
            return new Models.ChartOfAccount
            {
                Id = source.Id,
                Name = source.Name,
                AccountNo = source.AccountNo,
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.ChartOfAccount CreateFrom(this Models.ChartOfAccount source)
        {
            return new DomainModels.ChartOfAccount
            {
                Id = source.Id,
                Name = source.Name,
                AccountNo = source.AccountNo,
            };
        }

        #endregion
    }
}