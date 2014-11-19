using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.Web.Models;

namespace MPC.Web.ModelMappers
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
        public static ApiModels.Markup CreateFrom(this DomainModels.Markup source)
        {
            return new ApiModels.Markup
            {
                MarkUpId = source.MarkUpId,
                MarkUpName = source.MarkUpName,
                MarkUpRate = source.MarkUpRate,
            };
        }

        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.Markup CreateFrom(this ApiModels.Markup source)
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