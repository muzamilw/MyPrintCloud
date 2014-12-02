using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemProductDetail
    {
        public int ItemDetailId { get; set; }
        public Nullable<int> ItemId { get; set; }
        public Nullable<bool> isInternalActivity { get; set; }
        public Nullable<bool> isAutoCreateSupplierPO { get; set; }
        public Nullable<bool> isQtyLimit { get; set; }
        public Nullable<int> QtyLimit { get; set; }
        public Nullable<int> DeliveryTimeSupplier1 { get; set; }
        public Nullable<int> DeliveryTimeSupplier2 { get; set; }
        public Nullable<bool> isPrintItem { get; set; }
        public Nullable<bool> isAllowMarketBriefAttachment { get; set; }
        public string MarketBriefSuccessMessage { get; set; }
    }
}
