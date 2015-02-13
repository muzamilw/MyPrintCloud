using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Company Contact Request Model
    /// </summary>
    public class CompanyContactRequestModel : GetPagedListRequest
    {
        public string SearchFilter { get; set; }


        public long CompanyId { get; set; }
        public long TerritoryId { get; set; }

        /// <summary>
        /// Contact By Column for sorting
        /// </summary>

        public ContactByColumn ContactByColumn
        {
            get
            {
                return (ContactByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
