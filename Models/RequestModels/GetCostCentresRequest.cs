using MPC.Models.Common;

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
        /// Cost Centre Type
        /// </summary>
        public int? Type { get; set; }

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
