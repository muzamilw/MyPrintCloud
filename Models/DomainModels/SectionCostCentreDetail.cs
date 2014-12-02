﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class SectionCostCentreDetail
    {
        public int SectionCostCentreDetailId { get; set; }
        public int? SectionCostCentreId { get; set; }
        public int? StockId { get; set; }
        public int? SupplierId { get; set; }
        public double? Qty1 { get; set; }
        public double? Qty2 { get; set; }
        public double? Qty3 { get; set; }
        public double? CostPrice { get; set; }
        public int? ActualQtyUsed { get; set; }
        public string StockName { get; set; }
        public string Supplier { get; set; }

        public virtual SectionCostcentre SectionCostcentre { get; set; }
        public virtual StockItem StockItem { get; set; }
    }
}
