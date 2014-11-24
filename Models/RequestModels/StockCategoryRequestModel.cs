using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    public class StockCategoryRequestModel: GetPagedListRequest
    {
        public int StockCategoryId { get; set; }

        /// <summary>
        /// Paper Sheet By Column for sorting
        /// </summary>
        public StockCategoryByColumn StockCategoryOrderBy
        {
            get
            {
                return (StockCategoryByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
