using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class StockItemResponse
    {
        public IEnumerable<StockItem> StockItems { get; set; }
    }
}