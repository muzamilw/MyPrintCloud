using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    public class InvoicesRequestModel : GetPagedListRequest
    {
        
        /// <summary>
        /// Status of Screen(Shows which currently screen is working)
        /// </summary>
        public int Status { get; set; }
        
        /// <summary>
        /// List View Filter For Status Flag
        /// </summary>
        public int FilterFlag { get; set; }

        public string SearchString { get; set; }
        /// <summary>
        /// List View Filter For Order Type
        /// </summary>
        public int OrderTypeFilter { get; set; }
        /// <summary>
        /// Order By Column for sorting
        /// </summary>
        public InvoiceByColumn ItemOrderBy
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
