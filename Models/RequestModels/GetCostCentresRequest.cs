using MPC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Cost Centres Request Model
    /// </summary>
    public class GetCostCentresRequest : GetPagedListRequest
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }

        /// <summary>
        /// Order By Column for sorting
        /// </summary>
        public CostCentreOrderByColumn CostCentreOrderBy
        {
            get
            {
                return (CostCentreOrderByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
