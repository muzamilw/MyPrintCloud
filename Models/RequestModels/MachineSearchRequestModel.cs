using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Machine Search Request Model
    /// </summary>
    public class MachineSearchRequestModel : GetPagedListRequest
    {
        /// <summary>
        /// Machine By Column for sorting
        /// </summary>
        public MachineByColumn MachineOrderBy
        {
            get
            {
                return (MachineByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
