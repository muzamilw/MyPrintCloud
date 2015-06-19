namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Product Category WebApi Model
    /// </summary>
    public class ProductCategoryDropDown
    {
        public long ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public int? CategoryTypeId { get; set; }
        public bool IsArchived { get; set; }
    }
}