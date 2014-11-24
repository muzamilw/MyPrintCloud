using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    public class PaperSheetRequestModel : GetPagedListRequest
    {
        public int PaperSheetId { get; set; }

        /// <summary>
        /// Paper Sheet By Column for sorting
        /// </summary>
        public PaperSheetByColumn PaperSheetOrderBy
        {
            get
            {
                return (PaperSheetByColumn)SortBy;
            }
            set
            {
                SortBy = (short)value;
            }
        }
    }
}
