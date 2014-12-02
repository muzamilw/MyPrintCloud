using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Models;

namespace MPC.MIS.ModelMappers
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
        public static ApiModels.WeightUnitDropDown CreateFromDropDown(this DomainModels.WeightUnit source)
        {
            return new ApiModels.WeightUnitDropDown
            {
                Id = source.Id,
                UnitName = source.UnitName,
            };
        }


        #endregion
    }
}