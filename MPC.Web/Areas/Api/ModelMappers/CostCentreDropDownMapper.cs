using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class CostCentreDropDownMapper
    {
        #region Public
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static CostCentreDropDown CostCentreDropDownCreateFrom(this DomainModels.CostCentre source)
        {
            return new CostCentreDropDown
            {
                CostCentreId = source.CostCentreId,
                Name = source.Name,
            };
        }


        #endregion
    }
}