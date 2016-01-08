using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Machine Domain Model
    /// </summary>
    public class Machine
    {
        public int MachineId { get; set; }
        public string MachineName { get; set; }
        public int? MachineCatId { get; set; }
        public int? ColourHeads { get; set; }
        public bool? isPerfecting { get; set; }
        public double? SetupCharge { get; set; }
        public double? WashupPrice { get; set; }
        public double? WashupCost { get; set; }
        public double? MinInkDuctqty { get; set; }
        public double? worknturncharge { get; set; }
        public double? MakeReadyCost { get; set; }
        public int? DefaultFilmId { get; set; }
        public int? DefaultPlateId { get; set; }
        public int DefaultPaperId { get; set; }
        public bool? isfilmused { get; set; }
        public bool? isplateused { get; set; }
        public bool? ismakereadyused { get; set; }
        public bool? iswashupused { get; set; }
        public double? maximumsheetweight { get; set; }
        public double? maximumsheetheight { get; set; }
        public double? maximumsheetwidth { get; set; }
        public double? minimumsheetheight { get; set; }
        public double? minimumsheetwidth { get; set; }
        public double? gripdepth { get; set; }
        public int? gripsideorientaion { get; set; }
        public double? gutterdepth { get; set; }
        public double? headdepth { get; set; }
        public long? MarkupId { get; set; }
        public double? PressSizeRatio { get; set; }
        public string Description { get; set; }
        public double? Priority { get; set; }
        public bool? DirectCost { get; set; }
        public string Image { get; set; }
        public double? MinimumCharge { get; set; }
        public double? CostPerCut { get; set; }
        public double PricePerCut { get; set; }
        public bool? IsAdditionalOption { get; set; }
        public bool? IsDisabled { get; set; }
        public int LockedBy { get; set; }
        public int? CylinderSizeId { get; set; }
        public int? MaxItemAcrossCylinder { get; set; }
        public double? Web1MRCost { get; set; }
        public double? Web1MRPrice { get; set; }
        public double? Web2MRCost { get; set; }
        public double? Web2MRPrice { get; set; }
        public double? ReelMRCost { get; set; }
        public double ReelMRPrice { get; set; }
        public bool? IsMaxColorLimit { get; set; }
        public int? PressUtilization { get; set; }
        public double? MakeReadyPrice { get; set; }
        public double? InkChargeForUniqueColors { get; set; }
        public int CompanyId { get; set; }
        public int? FlagId { get; set; }
        public bool IsScheduleable { get; set; }
        public int SystemSiteId { get; set; }
        public int SpoilageType { get; set; }
        public double? SetupTime { get; set; }
        public double? TimePerCut { get; set; }
        public double? MakeReadyTime { get; set; }
        public double? WashupTime { get; set; }
        public double? ReelMakereadyTime { get; set; }     
     
        public long? LookupMethodId { get; set; }

        public int? SetupSpoilage { get; set; }
        public double? RunningSpoilage { get; set; }
        public double? CoverageHigh { get; set; }
        public double? CoverageMedium { get; set; }
        public double? CoverageLow { get; set; }

        public bool? isSheetFed { get; set; }
        public int? Passes { get; set; }
        public bool? IsSpotColor { get; set; }
        public bool? IsDigitalPress { get; set; }

        public virtual LookupMethod LookupMethod { get; set; }
        public IEnumerable<MachineInkCoverage> MachineInkCoverages { get; set; }
    }
}


