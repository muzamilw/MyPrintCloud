namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Template Font Domain Model
    /// </summary>
    public class TemplateFont
    {
        public long ProductFontId { get; set; }
        public long? ProductId { get; set; }
        public string FontName { get; set; }
        public string FontDisplayName { get; set; }
        public string FontFile { get; set; }
        public int? DisplayIndex { get; set; }
        public bool IsPrivateFont { get; set; }
        public bool? IsEnable { get; set; }
        public byte[] FontBytes { get; set; }
        public string FontPath { get; set; }
        public long? CustomerId { get; set; }
        public long? TerritoryId { get; set; }
        public virtual Template Template { get; set; }
    }
}
