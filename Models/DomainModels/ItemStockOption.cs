using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Item Stock Option Domain Model
    /// </summary>
    public class ItemStockOption
    {
        #region Persisted Properties
        public long ItemStockOptionId { get; set; }
        public long? ItemId { get; set; }
        public int? OptionSequence { get; set; }
        public long? StockId { get; set; }
        public string StockLabel { get; set; }
        public long? CompanyId { get; set; }
        public string ImageURL { get; set; }

        #endregion

        #region Reference Properties
        public virtual Item Item { get; set; }

        public virtual StockItem StockItem { get; set; }

        public virtual ICollection<ItemAddonCostCentre> ItemAddonCostCentres { get; set; }

        #endregion

        #region Additional Properties

        /// <summary>
        /// File in Base64
        /// </summary>
        [NotMapped]
        public string FileSource { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        [NotMapped]
        public string FileName { get; set; }

        #endregion
    }
}
