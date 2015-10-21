using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Item Search Request Model
    /// </summary>
    public class ItemSearchRequestModel : GetPagedListRequest
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public long? CompanyId { get; set; }

        /// <summary>
        /// Category Id
        /// </summary>
        public long? CategoryId { get; set; }


        public int? ProductType { get; set; }

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
