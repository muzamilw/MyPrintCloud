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
    }
}