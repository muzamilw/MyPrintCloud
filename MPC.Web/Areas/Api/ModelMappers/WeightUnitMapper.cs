using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Weight Unit Mapper
    /// </summary>
    public static class WeightUnitMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static WeightUnitDropDown CreateFromDropDown(this DomainModels.WeightUnit source)
        {
            return new WeightUnitDropDown
            {
                Id = source.Id,
                UnitName = source.UnitName,
            };
        }


        #endregion
    }
}