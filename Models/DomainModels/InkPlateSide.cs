namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Ink Plate Side Domain Model
    /// </summary>
    public class InkPlateSide
    {
        public int PlateInkId { get; set; }
        public string InkTitle { get; set; }
        public string PlateInkDescription { get; set; }
        public bool isDoubleSided { get; set; }
        public int PlateInkSide1 { get; set; }
        public int PlateInkSide2 { get; set; }
    }
}
