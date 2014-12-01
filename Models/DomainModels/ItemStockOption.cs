using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemStockOption
    {
        public long ItemStockOptionId { get; set; }
        public Nullable<long> ItemId { get; set; }
        public Nullable<int> OptionSequence { get; set; }
        public Nullable<long> StockId { get; set; }
        public string StockLabel { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public string ImageURL { get; set; }

        public virtual Item Item { get; set; }
    }
}
