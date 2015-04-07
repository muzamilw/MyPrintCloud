
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Models.Common;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// Item Section Repository 
    /// </summary>
    public interface IItemSectionRepository : IBaseRepository<ItemSection, long>
    {
        BestPressResponse GetBestPressResponse(ItemSection section);
        PtvDTO CalculatePTV(int ReversePTVRows, int ReversePTVCols, bool IsDoubleSided, bool ApplySwing, bool ApplyPressRestrict, double ItemHeight, double ItemWidth, double PrintHeight, double PrintWidth, int ColorBar,
                                    int Grip, double GripDepth, double HeadDepth, double PrintGutter, double ItemHorizontalGutter, double ItemVerticalGutter, bool IsWorknTurn, bool IsWorknTumble);
        PtvDTO DrawPTV(PrintViewOrientation strOrient, int ReversePTVRows, int ReversePTVCols, bool IsDoubleSided, bool IsWorknTurn, bool IsWorknTumble, bool ApplyPressRestrict, double ItemHeight, double ItemWidth, double PrintHeight, double PrintWidth, GripSide Grip, double GripDepth, double HeadDepth, double PrintGutter, double ItemHorizontalGutter, double ItemVerticalGutter);

        ItemSection GetUpdatedSectionWithSystemCostCenters(ItemSection currentSection, int PressId, List<SectionInkCoverage> AllInks);
    }
}
