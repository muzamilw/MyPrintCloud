namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Ink Plate Side Domain Model
    /// </summary>
    public class InkPlateSide
    {
        public int PlateInkId { get; set; }
        public string InkTitle { get; set; }
        public string PlateInkDescription { get; set; }
        public bool IsDoubleSided { get; set; }
        public int PlateInkSide1 { get; set; }
        public int PlateInkSide2 { get; set; }
    }
}
