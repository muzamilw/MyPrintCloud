using System.Globalization;
using DomainModels = MPC.Models.DomainModels;
using ApiModels = MPC.MIS.Models;

namespace MPC.MIS.ModelMappers
{
    public static class PaperSheetMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static ApiModels.PaperSheet CreateFrom(this DomainModels.PaperSize source)
        {
            return new ApiModels.PaperSheet
                   {
                       Area = source.Area,
                       Height = source.Height,
                       IsArchived = source.IsArchived,
                       IsFixed = source.IsFixed,
                       Name = source.Name,
                       PaperSizeId = source.PaperSizeId,
                       Region = source.Region,
                       SizeMeasure = source.SizeMeasure,
                       Width = source.Width
                   };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static DomainModels.PaperSize CreateFrom(this ApiModels.PaperSheet source)
        {
            return new DomainModels.PaperSize
                   {
                       Area = source.Height * source.Width,
                       Height = source.Height,
                       IsArchived = source.IsArchived == true ? source.IsArchived : false,
                       IsFixed = source.IsFixed,
                       Name = source.Name,
                       PaperSizeId = source.PaperSizeId,
                       Region = source.Region ?? CultureInfo.CurrentCulture.Name,
                       SizeMeasure = source.SizeMeasure ?? 0,
                       Width = source.Width
                   };
        }
    }
}