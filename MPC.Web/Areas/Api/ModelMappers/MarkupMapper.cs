using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
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
        public static Markup CreateFrom(this DomainModels.Markup source)
        {
            return new Markup
            {
                MarkUpId = source.MarkUpId,
                MarkUpName = source.MarkUpName,
                MarkUpRate = source.MarkUpRate,
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Markup CreateFrom(this Markup source)
        {
            return new DomainModels.Markup
            {
                MarkUpId = source.MarkUpId,
                MarkUpName = source.MarkUpName,
                MarkUpRate = source.MarkUpRate,
            };
        }
        #endregion
    }
}