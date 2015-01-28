namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Product Category WebApi Model
    /// </summary>
    public class ProductCategoryForTemplate
    {
        public long ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryTypeId { get; set; }
        public int? RegionId { get; set; }
        public decimal? ZoomFactor { get; set; }
        public decimal? ScaleFactor { get; set; }
    }
}