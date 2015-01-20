using MPC.MIS.Areas.Api.Models;
using DomainModels = MPC.Models.DomainModels;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Color Pallete Mapper
    /// </summary>
    public static class ColorPalleteMapper
    {
        #region Public
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ColorPallete CreateFrom(this DomainModels.ColorPallete source)
        {
            return new ColorPallete
            {
                PalleteId = source.PalleteId,
                Color1 = source.Color1,
                Color2 = source.Color2,
                Color3 = source.Color3,
                Color4 = source.Color4,
                Color5 = source.Color5,
                Color6 = source.Color6,
            };
        }
        
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static DomainModels.ColorPallete CreateFrom(this ColorPallete source)
        {
            return new DomainModels.ColorPallete
            {
                PalleteId = source.PalleteId,
                Color1 = source.Color1,
                Color2 = source.Color2,
                Color3 = source.Color3,
                Color4 = source.Color4,
                Color5 = source.Color5,
                Color6 = source.Color6,
            };
        }
        #endregion
    }
}