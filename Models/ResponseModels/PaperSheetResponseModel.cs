using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class PaperSheetResponseModel
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Paper Sheets
        /// </summary>
        public IEnumerable<PaperSize> PaperSheets { get; set; } 
    }
}
