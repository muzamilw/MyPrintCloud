namespace MPC.Models.DomainModels
{
    /// <summary>
    /// GetUsedFontsUpdatedResult Domain Model
    /// </summary>
    public class sp_GetUsedFontsUpdated_Result
    {
        public long ProductFontId { get; set; }
        public long? ProductId { get; set; }
        public string FontName { get; set; }
        public string FontDisplayName { get; set; }
        public string FontFile { get; set; }
        public int? DisplayIndex { get; set; }
        public bool IsPrivateFont { get; set; }
        public bool? IsEnable { get; set; }
        public long? CustomerID { get; set; }
        public string FontPath { get; set; }
        public int? FontBytes { get; set; }
    }
}
