using System.Linq;
using DomainModels = MPC.Models.DomainModels;
using DomainResponseModel = MPC.Models.ResponseModels;
using MPC.Models.ResponseModels;
using MPC.Models.DomainModels;

using APIDomainModels = MPC.MIS.Areas.Api.Models;
namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Machine WebApi Mapper
    /// </summary>
    public static class MachineMapper
    {

        #region Public

        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static APIDomainModels.MachineSearchResponse CreateFrom(this DomainResponseModel.MachineSearchResponse source)
        {
            return new APIDomainModels.MachineSearchResponse
            {
                Machines = source.Machines != null ? source.Machines.Select(machine => machine.CreateFrom()).ToList() : null,
                TotalCount = source.TotalCount
            };
        }

        public static APIDomainModels.MachineResponse CreateFrom(this DomainResponseModel.MachineResponseModel source)
        {
            return new APIDomainModels.MachineResponse
            {
                machine = source.machine == null ? null : source.machine.CreateFrom(),
                lookupMethods = source.lookupMethods == null ? null : source.lookupMethods.Select(s => s.CreateFrom()),
                Markups = source.Markups == null ? null : source.Markups.Select(s => s.CreateFrom()),
                StockItemforInk = source.StockItemforInk == null ? null : source.StockItemforInk.Select(s => s.CreateFromDetailForMachine()),
                //StockItemsForPaperSizePlate = source.StockItemsForPaperSizePlate.Select(s => s.CreateFromDetailForMachine()),
                MachineSpoilageItems = source.MachineSpoilageItems == null ? null : source.MachineSpoilageItems.Select(s => s.CreateFrom()),
                InkCoveragItems = source.InkCoveragItems == null ? null : source.InkCoveragItems.Select(s => s.CreateFrom()),
                deFaultPaperSizeName = source.deFaultPaperSizeName == null ? null : source.deFaultPaperSizeName,
                deFaultPlatesName = source.deFaultPlatesName == null ? null : source.deFaultPlatesName,
                CurrencySymbol = source.CurrencySymbol == null ? null : source.CurrencySymbol,
                WeightUnit = source.WeightUnit == null ? null : source.WeightUnit,
                LengthUnit = source.LengthUnit == null ? null : source.LengthUnit,
                MachineLookupMethods = source.MachineLookupMethods == null ? null : source.MachineLookupMethods.Select(s => s.CreateFrom()),
                GuilotinePtv = source.GuilotinePtv == null ? null : source.GuilotinePtv.Select(s => s.CreateFrom())
            };

        }
        /// <summary>
        /// Create From Domain Model
        /// </summary>


        public static APIDomainModels.Machine CreateFrom(this DomainModels.Machine source)
        {
            return new APIDomainModels.Machine
            {
                MachineId = source.MachineId,
                MachineName = source.MachineName,
                MachineCatId = source.MachineCatId,
                ColourHeads = source.ColourHeads,
                isPerfecting = source.isPerfecting,
                SetupCharge = source.SetupCharge,
                WashupPrice = source.WashupPrice,
                WashupCost = source.WashupCost,
                MinInkDuctqty = source.MinInkDuctqty,
                worknturncharge = source.worknturncharge,
                MakeReadyCost = source.MakeReadyCost,
                DefaultFilmId = source.DefaultFilmId,
                DefaultPlateId = source.DefaultPlateId,
                DefaultPaperId = source.DefaultPaperId,
                isfilmused = source.isfilmused,
                isplateused = source.isplateused,
                ismakereadyused = source.ismakereadyused,
                iswashupused = source.iswashupused,
                maximumsheetweight = source.maximumsheetweight,
                maximumsheetheight = source.maximumsheetheight,
                maximumsheetwidth = source.maximumsheetwidth,
                minimumsheetheight = source.minimumsheetheight,
                minimumsheetwidth = source.minimumsheetwidth,
                gripdepth = source.gripdepth,
                gripsideorientaion = source.gripsideorientaion,
                gutterdepth = source.gutterdepth,
                headdepth = source.headdepth,
                MarkupId = source.MarkupId,
                PressSizeRatio = source.PressSizeRatio,
                Description = source.Description,
                Priority = source.Priority,
                DirectCost = source.DirectCost,
                MinimumCharge = source.MinimumCharge,
                CostPerCut = source.CostPerCut,
                PricePerCut = source.PricePerCut,
                IsAdditionalOption = source.IsAdditionalOption,
                IsDisabled = source.IsDisabled,
                LockedBy = source.LockedBy,
                CylinderSizeId = source.CylinderSizeId,
                MaxItemAcrossCylinder = source.MaxItemAcrossCylinder,
                Web1MRCost = source.Web1MRCost,
                Web1MRPrice = source.Web1MRPrice,
                Web2MRCost = source.Web2MRPrice,
                Web2MRPrice = source.Web2MRPrice,
                ReelMRCost = source.ReelMRCost,
                ReelMRPrice = source.ReelMRPrice,
                IsMaxColorLimit = source.IsMaxColorLimit,
                PressUtilization = source.PressUtilization,
                MakeReadyPrice = source.MakeReadyPrice,
                InkChargeForUniqueColors = source.InkChargeForUniqueColors,
                CompanyId = source.CompanyId,
                FlagId = source.FlagId,
                IsScheduleable = source.IsScheduleable,
                SystemSiteId = source.SystemSiteId,
                SpoilageType = source.SpoilageType,
                SetupTime = source.SetupTime,
                TimePerCut = source.TimePerCut,
                MakeReadyTime = source.MakeReadyTime,
                WashupTime = source.WashupTime,
                ReelMakereadyTime = source.ReelMakereadyTime,
                LookupMethodId = source.LookupMethodId,
                RunningSpoilage = source.RunningSpoilage,
                SetupSpoilage = source.SetupSpoilage,
                CoverageHigh = source.CoverageHigh,
                CoverageLow = source.CoverageLow,
                CoverageMedium = source.CoverageMedium,
                isSheetFed = source.isSheetFed,
                Passes = source.Passes,
                IsSpotColor = source.IsSpotColor,
                MachineInkCoverages = source.MachineInkCoverages == null ? null : source.MachineInkCoverages.Select(g => g.CreateFrom()).ToList(),
                LookupMethod = source.LookupMethod.CreateFrom(),
            };
        }

        public static DomainModels.Machine CreateFrom(this APIDomainModels.Machine source)
        {
            return new DomainModels.Machine
            {
                MachineId = source.MachineId,
                MachineName = source.MachineName,
                MachineCatId = source.MachineCatId,
                ColourHeads = source.ColourHeads,
                isPerfecting = source.isPerfecting,
                SetupCharge = source.SetupCharge,
                WashupPrice = source.WashupPrice,
                WashupCost = source.WashupCost,
                MinInkDuctqty = source.MinInkDuctqty,
                worknturncharge = source.worknturncharge,
                MakeReadyCost = source.MakeReadyCost,
                DefaultFilmId = source.DefaultFilmId,
                DefaultPlateId = source.DefaultPlateId,
                DefaultPaperId = source.DefaultPaperId,
                isfilmused = source.isfilmused,
                isplateused = source.isplateused,
                ismakereadyused = source.ismakereadyused,
                iswashupused = source.iswashupused,
                Image = null,
                maximumsheetweight = source.maximumsheetweight,
                maximumsheetheight = source.maximumsheetheight,
                maximumsheetwidth = source.maximumsheetwidth,
                minimumsheetheight = source.minimumsheetheight,
                minimumsheetwidth = source.minimumsheetwidth,
                gripdepth = source.gripdepth,
                gripsideorientaion = source.gripsideorientaion,
                gutterdepth = source.gutterdepth,
                headdepth = source.headdepth,
                MarkupId = source.MarkupId,
                PressSizeRatio = source.PressSizeRatio,
                Description = source.Description,
                Priority = source.Priority,
                DirectCost = source.DirectCost,
                MinimumCharge = source.MinimumCharge,
                CostPerCut = source.CostPerCut,
                PricePerCut = source.PricePerCut,
                IsAdditionalOption = source.IsAdditionalOption,
                IsDisabled = source.IsDisabled,
                LockedBy = source.LockedBy,
                CylinderSizeId = source.CylinderSizeId,
                MaxItemAcrossCylinder = source.MaxItemAcrossCylinder,
                Web1MRCost = source.Web1MRCost,
                Web1MRPrice = source.Web1MRPrice,
                Web2MRCost = source.Web2MRPrice,
                Web2MRPrice = source.Web2MRPrice,
                ReelMRCost = source.ReelMRCost,
                ReelMRPrice = source.ReelMRPrice,
                IsMaxColorLimit = source.IsMaxColorLimit,
                PressUtilization = source.PressUtilization,
                MakeReadyPrice = source.MakeReadyPrice,
                InkChargeForUniqueColors = source.InkChargeForUniqueColors,
                CompanyId = source.CompanyId,
                FlagId = source.FlagId,
                IsScheduleable = source.IsScheduleable,
                SystemSiteId = source.SystemSiteId,
                SpoilageType = source.SpoilageType,
                SetupTime = source.SetupTime,
                TimePerCut = source.TimePerCut,
                MakeReadyTime = source.MakeReadyTime,
                WashupTime = source.WashupTime,
                ReelMakereadyTime = source.ReelMakereadyTime,
                LookupMethodId = source.LookupMethodId,
                RunningSpoilage = source.RunningSpoilage,
                SetupSpoilage = source.SetupSpoilage,
                CoverageHigh = source.CoverageHigh,
                CoverageLow = source.CoverageLow,
                CoverageMedium = source.CoverageMedium,
                isSheetFed = source.isSheetFed,
                Passes = source.Passes,
                IsSpotColor = source.IsSpotColor,
                MachineInkCoverages = source.MachineInkCoverages != null ? source.MachineInkCoverages.Select(g => g.CreateFrom()).ToList() : null
            };
        }

        /// <summary>
        /// Used In Order
        /// </summary>
        public static APIDomainModels.Machine CreateFromForOrder(this Machine source)
        {
            return new APIDomainModels.Machine
            {
                MachineId = source.MachineId,
                MachineName = source.MachineName,
                MachineCatId = source.MachineCatId,
                maximumsheetheight = source.maximumsheetheight,
                maximumsheetwidth = source.maximumsheetwidth,
                ColourHeads = source.ColourHeads,
                IsSpotColor = source.IsSpotColor,
                Passes = source.Passes
            };
        }
        #endregion

    }
}


