namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Template Page Api Model
    /// </summary>
    public class TemplatePage
    {
        public long ProductPageId { get; set; }
        public long? ProductId { get; set; }
        public int? PageNo { get; set; }
        public int? PageType { get; set; }
        public int? Orientation { get; set; }
        public int? BackGroundType { get; set; }
        public string BackgroundFileName { get; set; }
        public int? ColorC { get; set; }
        public int? ColorM { get; set; }
        public int? ColorY { get; set; }
        public string PageName { get; set; }
        public int? ColorK { get; set; }
        public bool? IsPrintable { get; set; }
        public bool? HasOverlayObjects { get; set; }

        public double? Width { get; set; }

        public double? Height { get; set; }
    }
}