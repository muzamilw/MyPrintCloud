namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Template Color Style Domain Model
    /// </summary>
    public class TemplateColorStyle
    {
        public int PelleteId { get; set; }
        public int? ProductId { get; set; }
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
        public int? CustomerId { get; set; }

        public virtual Template Template { get; set; }
    }
}
