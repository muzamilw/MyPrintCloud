using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.Webstore.ViewModels
{
    public class ProductPriceMatrixViewModel
    {
        public string Quantity { get; set; }
     
        public double Price { get; set; }

        public int ItemID { get; set; }
        public double QtyRangeFrom { get; set; }
        public double QtyRangeTo { get; set; }

       
    }


    public class ItemStockOptionList
    {
        public string StockLabel { get; set; }

        public int ItemID { get; set; }
    }
}