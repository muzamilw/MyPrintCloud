using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Order Search Request Model
    /// </summary>
    public class GetOrdersRequest : GetPagedListRequest
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// Status of Screen(Shows which currently screen is working)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// Order By Column for sorting
        /// </summary>
        public OrderByColumn ItemOrderBy
        {
            get
            {
                return (OrderByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
