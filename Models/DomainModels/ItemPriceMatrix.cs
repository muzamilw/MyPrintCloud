using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemPriceMatrix
    {
        public long PriceMatrixId { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<long> ItemId { get; set; }
        public Nullable<double> PricePaperType1 { get; set; }
        public Nullable<double> PricePaperType2 { get; set; }
        public Nullable<double> PricePaperType3 { get; set; }
        public Nullable<bool> IsDiscounted { get; set; }
        public Nullable<int> QtyRangeFrom { get; set; }
        public Nullable<int> QtyRangeTo { get; set; }
        public Nullable<int> SupplierId { get; set; }
        public Nullable<double> PriceStockType4 { get; set; }
        public Nullable<double> PriceStockType5 { get; set; }
        public Nullable<double> PriceStockType6 { get; set; }
        public Nullable<double> PriceStockType7 { get; set; }
        public Nullable<double> PriceStockType8 { get; set; }
        public Nullable<double> PriceStockType9 { get; set; }
        public Nullable<double> PriceStockType10 { get; set; }
        public Nullable<double> PriceStockType11 { get; set; }
        public Nullable<int> FlagId { get; set; }
        public Nullable<int> SupplierSequence { get; set; }
        public Nullable<int> ContactCompanyId { get; set; }

        public virtual Item Item { get; set; }
    }
}
