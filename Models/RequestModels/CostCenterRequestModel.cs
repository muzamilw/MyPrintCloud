using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    public class CostCenterRequestModel : GetPagedListRequest
    {
        public long CostCenterId { get; set; }
        public long CostCenterType { get; set; }
        /// <summary>
        /// Paper Sheet By Column for sorting
        /// </summary>
        public CostCentersColumns CostCenterOrderBy
        {
            get
            {
                return (CostCentersColumns)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
