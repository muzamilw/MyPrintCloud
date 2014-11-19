using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Models;

namespace MPC.MIS.ModelMappers
{
    /// <summary>
    /// Markup Mapper
    /// </summary>
    public static class MarkupMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static Models.Markup CreateFrom(this DomainModels.MarkUp source)
        {
            return new Models.Markup
            {
                MarkUpId = source.MarkUpId,
                MarkUpName = source.MarkUpName,
                MarkUpRate = source.MarkUpRate,
            };
        }

        #endregion
    }
}