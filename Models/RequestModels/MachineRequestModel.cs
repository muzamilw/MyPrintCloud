using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    public class MachineRequestModel : GetPagedListRequest
    {
        public long MachineId { get; set; }

        /// <summary>
        /// Paper Sheet By Column for sorting
        /// </summary>
        public MachineListColumns MachineOrderBy
        {
            get
            {
                return (MachineListColumns)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
