using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Ink Plate Side Mapper
    /// </summary>
    public static class InkPlateSideMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static InkPlateSide CreateFromDropDown(this MPC.Models.DomainModels.InkPlateSide source)
        {
            return new InkPlateSide
            {
                PlateInkId = source.PlateInkId,
                InkTitle = source.InkTitle,
                IsDoubleSided = source.isDoubleSided,
                PlateInkSide1 = source.PlateInkSide1,
                PlateInkSide2 = source.PlateInkSide2
            };
        }
        #endregion
    }
}