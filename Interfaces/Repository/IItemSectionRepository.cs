
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
        //BestPressResponse GetBestPressResponse(ItemSection section);
        //PtvDTO CalculatePTV(int ReversePTVRows, int ReversePTVCols, bool IsDoubleSided, bool ApplySwing, bool ApplyPressRestrict, double ItemHeight, double ItemWidth, double PrintHeight, double PrintWidth, int ColorBar,
        //                            int Grip, double GripDepth, double HeadDepth, double PrintGutter, double ItemHorizontalGutter, double ItemVerticalGutter, bool IsWorknTurn, bool IsWorknTumble);
        //PtvDTO DrawPTV(PrintViewOrientation strOrient, int ReversePTVRows, int ReversePTVCols, bool IsDoubleSided, bool IsWorknTurn, bool IsWorknTumble, bool ApplyPressRestrict, double ItemHeight, double ItemWidth, double PrintHeight, double PrintWidth, GripSide Grip, double GripDepth, double HeadDepth, double PrintGutter, double ItemHorizontalGutter, double ItemVerticalGutter);

        //ItemSection GetUpdatedSectionWithSystemCostCenters(ItemSection currentSection, int PressId, List<SectionInkCoverage> AllInks);



        Machine GetPressById(int PressId);
        CostCentre GetCostCenterBySystemType(int systemType);
        StockItem GetStockById(long StockItemId);
        LookupMethod GetLookupMethodById(long MethodId);
        MachineClickChargeLookup GetMachineClickChargeById(long Id);
        MachineSpeedWeightLookup GetMachineSpeedWeightLookup(long Id);
        MachinePerHourLookup GetMachinePerHourLookup(long Id);
        MachineClickChargeZone GetMachineClickChargeZone(long Id);
        Markup GetMarkupById(int Id);
        List<StockCostAndPrice> GetStockPricingByStockId(int StockId, bool isPrice);
        MachineSpoilage GetMachineSpoilageByMachineIdAndColors(int machineId, int Inks);
        InkCoverageGroup GetInkCoverageById(int Id);
        PaperSize GetPaperSizeById(int Id);
        MachineGuillotineCalc GetGuillotineCalcMethod(long iMethodId);
        MachineGuilotinePtv GetGuillotinePtv(long guillotineId, int orderPtv);
        string GetWeightUnitName(int UnitID);
        string GetLengthUnitName(int UnitID);
        MPC.Models.DomainModels.LengthUnit GetLengthUnitByInput(int InputUnit);
        MPC.Models.DomainModels.WeightUnit GetWeightUnitByInput(int InputUnit);
        JobPreference GetJobPreferences(int SystemSiteID);
        long GetSystemCostCentreID(SystemCostCenterTypes SystemType);
        List<Machine> GetEnabledPresses(double sectionHeight, double sectionWidth);
        List<MachineSpoilage> GetSpoilageByPressId(int PressId);
        List<CostCentre> GetUserDefinedCostCentres();
        /// <summary>
        /// get item section no 1
        /// </summary>
        /// <param name="StockOptionID"></param>
        /// <param name="CompanyID"></param>
        /// <returns></returns>
        ItemSection GetFirstSectionOfItem(long ItemId);
        ItemSection GetSectionByItemId(long ItemId);
    }
}
