namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Imposition Profile Domain Model
    /// </summary>
    public class ImpositionProfile
    {
        public long ImpositionId { get; set; }
        public string Title { get; set; }
        public long? SheetSizeId { get; set; }
        public double? SheetHeight { get; set; }
        public double? SheetWidth { get; set; }
        public long? ItemSizeId { get; set; }
        public double? ItemHeight { get; set; }
        public double? ItemWidth { get; set; }
        public double? Area { get; set; }
        public int? PTV { get; set; }
        public string Region { get; set; }
        public long? OrganisationId { get; set; }
        public bool? isPortrait { get; set; }
    }
}
