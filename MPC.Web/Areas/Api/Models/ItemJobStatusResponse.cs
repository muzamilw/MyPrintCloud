using System.Collections.Generic;
using MPC.Models.ResponseModels;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Job Status API Response
    /// </summary>
    public class ItemJobStatusResponse
    {
        public IEnumerable<ItemForItemJobStatus> Items { get; set; }
        public string CurrencySymbol { get; set; }
        public string ProductionBoardLabel1 { get; set; }
        public string ProductionBoardLabel2 { get; set; }
        public string ProductionBoardLabel3 { get; set; }
        public string ProductionBoardLabel4 { get; set; }
        public string ProductionBoardLabel5 { get; set; }
        public string ProductionBoardLabel6 { get; set; }
    }
}