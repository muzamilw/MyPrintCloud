using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class MachineResponse
    {
        public Machine machine { get; set; }
        public IEnumerable<LookupMethod> lookupMethods { get; set; }
        public IEnumerable<Markup> Markups { get; set; }
        public IEnumerable<StockItem> StockItemforInk { get; set; }
        //public IEnumerable<MachineSpoilage>
        public virtual IEnumerable<MachineResource> MachineResources { get; set; }

    }
}