using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    public class CompanyRequestModel : GetPagedListRequest
    {
        public int CompanyId { get; set; }

        /// <summary>
        /// Company By Column for sorting
        /// </summary>
        public CompanyByColumn CompanyByColumn
        {
            get
            {
                return (CompanyByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
