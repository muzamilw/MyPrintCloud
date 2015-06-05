namespace MPC.MIS.Areas.Api.Models
{
    public class PaperSheet
    {
        #region Persisted Properties
        public int PaperSizeId { get; set; }
        public string Name { get; set; }
        public double? Height { get; set; }
        public double? Width { get; set; }
        public int? SizeMeasure { get; set; }
        public double? Area { get; set; }
        public int IsFixed { get; set; }
        public string Region { get; set; }
        public bool? IsArchived { get; set; }

        public bool? IsImperical { get; set; }
        #endregion
    }
}