using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    public class ItemStockOption
    {
        public long ItemStockOptionId { get; set; }
        public long? ItemId { get; set; }
        public int? OptionSequence { get; set; }
        public long? StockId { get; set; }
        public string StockLabel { get; set; }
        public long? CompanyId { get; set; }
        public string ImageURL { get; set; }

        public virtual Item Item { get; set; }

        public virtual StockItem StockItem { get; set; }

        public virtual ICollection<ItemAddonCostCentre> ItemAddonCostCentres { get; set; }
    }
}
