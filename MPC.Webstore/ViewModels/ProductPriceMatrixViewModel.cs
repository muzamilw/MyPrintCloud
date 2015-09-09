using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.Models.Common;

namespace MPC.Webstore.ViewModels
{
    public class ProductPriceMatrixViewModel
    {
        public string Quantity { get; set; }
     
        public string Price { get; set; }

        public int ItemID { get; set; }
        public double QtyRangeFrom { get; set; }
        public double QtyRangeTo { get; set; }

       
    }
    public class ItemStockOptionList
    {
        public string StockLabel { get; set; }

        public int ItemID { get; set; }
    }

    public class AddOnCostCenterViewModel
    {
        public double? SetupCost { get; set; }

        public double? MinimumCost { get; set; }

        public int Type { get; set; }

        public double ActualPrice { get; set; }

        public long Id { get; set; }
        public long CostCenterId { get; set; }
        public long StockOptionId { get; set; }
        public string Description { get; set; }

        public bool isChecked { get; set; }

        // this will be set only in modify product case 
        public string CostCentreJasonData { get; set; }

        public int QuantitySourceType { get; set; }
        public int TimeSourceType { get; set; }
        public long ItemStockOptionId { get; set; }
    }
}