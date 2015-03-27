using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    public class CompanyRequestModel : GetPagedListRequest
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// Company Type
        /// </summary>
        public long? CustomerType { get; set; }

        /// <summary>
        /// Customer
        /// </summary>
        public int IsCustomer { get; set; }

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
