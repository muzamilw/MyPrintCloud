using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Paper Basis Area Mapper
    /// </summary>
    public static class PaperBasisAreaMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static PaperBasisAreaDropDown CreateFromDropDown(this DomainModels.PaperBasisArea source)
        {
            return new PaperBasisAreaDropDown
            {
                PaperBasisAreaId = source.PaperBasisAreaId,
                Name = source.Name + " - " + source.Value,
            };
        }

        #endregion
    }
}