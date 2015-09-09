using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Stock Item Request Model
    /// </summary>
    public class StockItemRequestModel: GetPagedListRequest
    {

        /// <summary>
        ///Category Id
        /// </summary>
        public long? CategoryId { get; set; }
        
        /// <summary>
        /// Stock Category By Column for sorting
        /// </summary>
        public InventoryByColumn StockItemOrderBy
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
