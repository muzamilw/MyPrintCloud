using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemVDPPrice
    {
        public long ItemVDPPriceId { get; set; }
        public Nullable<int> ClickRangeTo { get; set; }
        public Nullable<int> ClickRangeFrom { get; set; }
        public Nullable<double> PricePerClick { get; set; }
        public Nullable<double> SetupCharge { get; set; }
        public Nullable<long> ItemId { get; set; }

        public virtual Item Item { get; set; }
    }
}
