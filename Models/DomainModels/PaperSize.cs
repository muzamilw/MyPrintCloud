namespace MPC.Models.DomainModels
{
    public class PaperSize
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
        public long? OrganisationId { get; set; }

        /// <summary>
        /// is Imperical
        /// </summary>
        public bool? IsImperical { get; set; }

        #endregion
    }
}
