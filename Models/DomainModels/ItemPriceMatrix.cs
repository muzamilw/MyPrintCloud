using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Item Price Matrix Domain Model
    /// </summary>
    public class ItemPriceMatrix
    {
        public long PriceMatrixId { get; set; }
        public int? Quantity { get; set; }
        public double? Price { get; set; }
        public long? ItemId { get; set; }
        public double? PricePaperType1 { get; set; }
        public double? PricePaperType2 { get; set; }
        public double? PricePaperType3 { get; set; }
        public int? QtyRangeFrom { get; set; }
        public int? QtyRangeTo { get; set; }
        public int? SupplierId { get; set; }
        public double? PriceStockType4 { get; set; }
        public double? PriceStockType5 { get; set; }
        public double? PriceStockType6 { get; set; }
        public double? PriceStockType7 { get; set; }
        public double? PriceStockType8 { get; set; }
        public double? PriceStockType9 { get; set; }
        public double? PriceStockType10 { get; set; }
        public double? PriceStockType11 { get; set; }
        public int? FlagId { get; set; }
        public int? SupplierSequence { get; set; }
        public virtual Item Item { get; set; }

        #region Public

        /// <summary>
        /// Creates Copy of Entity
        /// </summary>
        public void Clone(ItemPriceMatrix target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemPriceMatrixClone_InvalidItem, "target");
            }

            target.FlagId = FlagId;
            target.SupplierId = SupplierId;
            target.SupplierSequence = SupplierSequence;
            target.PricePaperType1 = PricePaperType1;
            target.PricePaperType2 = PricePaperType2;
            target.PricePaperType3 = PricePaperType3;
            target.PriceStockType4 = PriceStockType4;
            target.PriceStockType5 = PriceStockType5;
            target.PriceStockType6 = PriceStockType6;
            target.PriceStockType7 = PriceStockType7;
            target.PriceStockType8 = PriceStockType8;
            target.PriceStockType9 = PriceStockType9;
            target.PriceStockType10 = PriceStockType10;
            target.PriceStockType11 = PriceStockType11;
            target.Quantity = Quantity;
            target.QtyRangeFrom = QtyRangeFrom;
            target.QtyRangeTo = QtyRangeTo;
            target.Price = Price;
        }

        #endregion
    }
}
