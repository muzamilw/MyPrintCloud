using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.ViewModels
{
    public class StockItemViewModel
    {
        public double InStockValue { get; set; }
        public bool isAllowBackOrder { get; set; }
        public string StockTextToDisplay { get; set; }
        public long StockOptionId { get; set; }
        public long StockId { get; set; }
        public bool isItemInStock { get; set; }
    }
}