namespace MPC.MIS.Models
{
    /// <summary>
    /// Stock Sub Category DropDown
    /// </summary>
    public class StockSubCategoryDropDown
    {
        /// <summary>
        /// Sub Category Id
        /// </summary>
        public long SubCategoryId { get; set; }

        /// <summary>
        /// Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Category Id
        /// </summary>
        public long CategoryId { get; set; }
    }
}