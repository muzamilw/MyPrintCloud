namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Job Preference Domain Model
    /// </summary>
    public class JobPreference
    {
        public int Id { get; set; }
        public bool? IsDefaultInkColorUsed { get; set; }
        public bool? IsDefaultFilmUsed { get; set; }
        public bool? IsdefaultPlateUsed { get; set; }
        public bool? IsDefaultMakereadyUsed { get; set; }
        public bool? IsDefaultWashupUsed { get; set; }
        public bool? IsDefaultPressInstruction { get; set; }
        public bool? IsWorkingSize { get; set; }
        public bool? IsItemSize { get; set; }
        public bool? IsImpressionCount { get; set; }
        public bool? IsNumberOfPasses { get; set; }
        public bool? IsPrintSheetQty { get; set; }
        public bool? IsDefaultStockDetail { get; set; }
        public bool? IsOrderSheetSize { get; set; }
        public bool? IsSpoilageAllowed { get; set; }
        public bool? IsPaperWeight { get; set; }
        public bool? IsPaperSheetQty { get; set; }
        public bool? IsDefaultGuilotine { get; set; }
        public bool? IsGuilotineWorkingSize { get; set; }
        public bool? IsGuilotineItemSize { get; set; }
        public bool? IsNoOfTrims { get; set; }
        public bool? IsNoOfCuts { get; set; }
        public bool? IsInksEstTime { get; set; }
        public bool? InFilmEstTime { get; set; }
        public bool? IsPlateEstTime { get; set; }
        public bool? IsWashupEstTime { get; set; }
        public bool? IsMakereadyEstTime { get; set; }
        public bool? IsPressEstTime { get; set; }
        public bool? IsPaperEstTime { get; set; }
        public bool? IsGuillotineEstTime { get; set; }
        public bool? IsReelMakeReady { get; set; }
        public bool? IsReelMakeReadyTime { get; set; }
        public int SystemSiteId { get; set; }
    }
}
