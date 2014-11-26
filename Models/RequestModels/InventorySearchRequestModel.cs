using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Inventory Search Request Model
    /// </summary>
    public class InventorySearchRequestModel : GetPagedListRequest
    {
        /// <summary>
        /// Category Id
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Sub Category Id
        /// </summary>
        public int? SubCategoryId { get; set; }

        /// <summary>
        /// Inventory By Column for sorting
        /// </summary>
        public InventoryByColumn InventoryOrderBy
        {
            get
            {
                return (InventoryByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
