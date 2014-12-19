using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    public class PaperSheetResponse
    {
        public IEnumerable<PaperSize> PaperSizes { get; set; }
        public int RowCount { get; set; }
    }
}
