namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Stock Sub Category DropDown
    /// </summary>
    public class StockSubCategoryDropDown
    {
        /// <summary>
        /// Sub Category Id
        /// </summary>
        public int SubCategoryId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category Id
        /// </summary>
        public int CategoryId { get; set; }
    }
}