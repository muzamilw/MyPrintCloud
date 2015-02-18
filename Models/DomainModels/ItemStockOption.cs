using System;
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

        /// <summary>
        /// File Source Bytes - byte[] representation of Base64 string FileSource
        /// </summary>
        public byte[] FileSourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(FileSource))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = FileSource.IndexOf(',');
                
                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (FileSource.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = FileSource.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        #endregion

        #region Public

        /// <summary>
        /// Create Clone of Item Stock Option
        /// </summary>
        public void Clone(ItemStockOption target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemStockOption_InvalidItemStockOption, "target");
            }

            target.StockId = StockId;
            target.StockLabel = StockLabel;
            target.OptionSequence = OptionSequence;
            target.ImageURL = ImageURL;

        }

        #endregion
    }
}
