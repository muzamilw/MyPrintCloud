
using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    public class DeliveryNotesRequest : GetPagedListRequest
    {

        public string SearchingString { get; set; }

        public short? Status { get; set; }
        /// <summary>
        /// Item By Column for sorting
        /// </summary>
        public DeliveryNoteByColumn ItemOrderBy
        {
            get
            {
                return (DeliveryNoteByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
