using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item Price Matrix mapper
    /// </summary>
    public static class ItemPriceMatrixMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemPriceMatrix source, ItemPriceMatrix target, Item targetItem)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ItemId = source.ItemId;
            target.SupplierId = source.SupplierId;
            target.SupplierSequence = source.SupplierSequence;
            target.FlagId = source.FlagId;
            
            if (targetItem.IsQtyRanged.HasValue && targetItem.IsQtyRanged.Value)
            {
                target.QtyRangeFrom = source.QtyRangeFrom;
                target.QtyRangeTo = source.QtyRangeTo;
            }
            else
            {
                target.Quantity = source.Quantity;
            }
            
            if (targetItem.ItemStockOptions.Count > 0)
            {
                foreach (ItemStockOption stockOption in targetItem.ItemStockOptions)
                {
                    if (stockOption.OptionSequence.HasValue)
                    {
                        switch (stockOption.OptionSequence.Value)
                        {
                            case 1:
                                target.PricePaperType1 = source.PricePaperType1;
                                break;
                            case 2:
                                target.PricePaperType2 = source.PricePaperType2;
                                break;
                            case 3:
                                target.PricePaperType3 = source.PricePaperType3;
                                break;
                            case 4:
                                target.PriceStockType4 = source.PriceStockType4;
                                break;
                            case 5:
                                target.PriceStockType5 = source.PriceStockType5;
                                break;
                            case 6:
                                target.PriceStockType6 = source.PriceStockType6;
                                break;
                            case 7:
                                target.PriceStockType7 = source.PriceStockType7;
                                break;
                            case 8:
                                target.PriceStockType8 = source.PriceStockType8;
                                break;
                            case 9:
                                target.PriceStockType9 = source.PriceStockType9;
                                break;
                            case 10:
                                target.PriceStockType10 = source.PriceStockType10;
                                break;
                            case 11:
                                target.PriceStockType11 = source.PriceStockType11;
                                break;
                        }
                    }
                }    
            }

        }

        #endregion
    }
}
