using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class MachineResponse
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
        
        public IEnumerable<InkCoverageGroup> InkCoveragItems { get; set; }
        public IEnumerable<MachineSpoilage> MachineSpoilageItems { get; set; }

        public IEnumerable<MachineLookupMethod> MachineLookupMethods { get; set; }

        public IEnumerable<MachineGuilotinePtv> GuilotinePtv { get; set; }
        

    }
}