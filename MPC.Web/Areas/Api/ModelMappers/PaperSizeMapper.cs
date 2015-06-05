using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Paper Size Mapper
    /// </summary>
    public static class PaperSizeMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static PaperSizeDropDown CreateFromDropDown(this MPC.Models.DomainModels.PaperSize source)
        {
            return new PaperSizeDropDown
            {
                PaperSizeId = source.PaperSizeId,
                Name = source.Name,
                Height = source.Height,
                Width = source.Width
            };
        }
        #endregion
    }
}