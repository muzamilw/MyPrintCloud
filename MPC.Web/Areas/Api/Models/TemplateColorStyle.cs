
namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Web Api Model
    /// </summary>
    public class TemplateColorStyle
    {
        public long PelleteId { get; set; }
        public long? ProductId { get; set; }
        public string Name { get; set; }
        public int? ColorC { get; set; }
        public int? ColorM { get; set; }
        public int? ColorY { get; set; }
        public int? ColorK { get; set; }
        public string SpotColor { get; set; }
        public bool? IsSpotColor { get; set; }
        public int? Field1 { get; set; }
        public string ColorHex { get; set; }
        public bool? IsColorActive { get; set; }
        public long? CustomerId { get; set; }
        public long? TerritoryId { get; set; }
    }
}