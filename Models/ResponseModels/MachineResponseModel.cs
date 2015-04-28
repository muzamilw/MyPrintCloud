﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.ResponseModels
{
    public class MachineResponseModel
    {
        public Machine machine { get; set; }
        public string CurrencySymbol { get; set; }
        public string WeightUnit { get; set; }
        public string LengthUnit { get; set; }
        public string deFaultPaperSizeName { get; set; }
        public string deFaultPlatesName { get; set; }
        public IEnumerable<LookupMethod> lookupMethods { get; set; }
        public IEnumerable<Markup> Markups { get; set; }
        public IEnumerable<StockItem> StockItemforInk { get; set; }
       // public IEnumerable<StockItem> StockItemsForPaperSizePlate { get; set; }
        //public virtual IEnumerable<MachineResource> MachineResources { get; set; }
        public IEnumerable<MachineSpoilage> MachineSpoilageItems { get; set; }
        public IEnumerable<InkCoverageGroup> InkCoveragItems { get; set; }
    }
}
