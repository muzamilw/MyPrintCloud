using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Length Unit Mapper
    /// </summary>
    public static class LengthUnitMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static LengthUnit CreateFromDropDown(this DomainModels.LengthUnit source)
        {
            return new LengthUnit
            {
                Id = source.Id,
                UnitName = source.UnitName,
            };
        }

        #endregion
    }
}