using MPC.MIS.Areas.Api.Models;
using System.Linq;
using CommonModels = MPC.Models.Common;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class PtvDTOMapper
    {
        public static PtvDTO CreateFrom(this CommonModels.PtvDTO source)
        {
            return new PtvDTO
            {
                LandScapeCols = source.LandScapeCols,
                LandscapePTV = source.LandscapePTV,
                LandScapeRows = source.LandScapeRows,
                LandScapeSwing = source.LandScapeSwing,
                PortraitCols = source.PortraitCols,
                PortraitPTV = source.PortraitPTV,
                PortraitRows = source.PortraitRows,
                PortraitSwing = source.PortraitSwing,
                Side1Image = source.Side1Image,
                Side2Image = source.Side2Image
            };
        }

    }
}