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
        public static LengthUnitDropDown CreateFromDropDown(this DomainModels.LengthUnit source)
        {
            return new LengthUnitDropDown
            {
                Id = source.Id,
                UnitName = source.UnitName,
            };
        }

        #endregion
    }
}