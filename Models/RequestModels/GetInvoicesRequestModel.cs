using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Request Model for Invoices
    /// </summary>
    public class GetInvoicesRequestModel : GetPagedListRequest
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Invoice By Column for sorting
        /// </summary>
        public InvoiceByColumn ItemInvoiceBy
        {
            get
            {
                return (InvoiceByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
