using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    public class GoodsReceivedNoteRequestModel : GetPagedListRequest
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public int CompanyId { get; set; }
        /// <summary>
        /// Status of Screen(Shows which currently screen is working)
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// List View Filter For Status Flag
        /// </summary>
        public int FilterFlag { get; set; }
        /// <summary>
        /// List View Filter For Order Type
        /// </summary>
        public int OrderTypeFilter { get; set; }
        /// <summary>
        /// Order By Column for sorting
        /// </summary>
        public GoodsReceivedNoteByColumn ItemOrderBy
        {
            get
            {
                return (GoodsReceivedNoteByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
