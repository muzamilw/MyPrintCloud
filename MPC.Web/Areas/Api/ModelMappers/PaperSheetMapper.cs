using System.Globalization;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class PaperSheetMapper
    {
        /// <summary>
        /// Crete From Domain Model
        /// </summary>
        public static PaperSheet CreateFrom(this MPC.Models.DomainModels.PaperSize source)
        {
            return new PaperSheet
                   {
                       Area = source.Area,
                       Height = source.Height,
                       IsArchived = source.IsArchived,
                       IsFixed = source.IsFixed,
                       Name = source.Name,
                       PaperSizeId = source.PaperSizeId,
                       Region = source.Region,
                       SizeMeasure = source.SizeMeasure,
                       Width = source.Width,
                       IsImperical = source.IsImperical
                   };
        }
        /// <summary>
        /// Crete From Web Model
        /// </summary>
        public static MPC.Models.DomainModels.PaperSize CreateFrom(this PaperSheet source)
        {
            return new MPC.Models.DomainModels.PaperSize
                   {
                       Area = source.Height * source.Width,
                       Height = source.Height,
                       IsArchived = source.IsArchived == true ? source.IsArchived : false,
                       IsFixed = source.IsFixed,
                       Name = source.Name,
                       PaperSizeId = source.PaperSizeId,
                       Region = source.Region,
                       SizeMeasure = source.SizeMeasure ?? 0,
                       Width = source.Width,
                       IsImperical = source.IsImperical
                   };
        }
    }
}