using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class ItemAddonCostCentre
    {
        public int ProductAddOnId { get; set; }
        public Nullable<long> ItemStockOptionId { get; set; }
        public Nullable<long> CostCentreId { get; set; }
        public Nullable<double> DiscountPercentage { get; set; }
        public Nullable<bool> IsDiscounted { get; set; }
        public Nullable<int> Sequence { get; set; }
        public Nullable<bool> IsMandatory { get; set; }

        public virtual CostCentre CostCentre { get; set; }
    }
}
