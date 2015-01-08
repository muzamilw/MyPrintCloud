using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Item Search Request Model
    /// </summary>
    public class CompanyProductSearchRequestModel : GetPagedListRequest
    {
        public long CompanyId { get; set; }
        /// <summary>
        /// Item By Column for sorting
        /// </summary>
        public ItemByColumn ItemOrderBy
        {
            get
            {
                return (ItemByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
