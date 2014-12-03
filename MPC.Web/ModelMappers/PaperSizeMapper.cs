using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.ModelMappers
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
        public static Models.PaperSizeDropDown CreateFromDropDown(this DomainModels.PaperSize source)
        {
            return new Models.PaperSizeDropDown
            {
                PaperSizeId = source.PaperSizeId,
                Name = source.Name + " " + source.Width + " x " + source.Height,
            };
        }
        #endregion
    }
}