using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item Stock Option mapper
    /// </summary>
    public static class ItemStockOptionMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemStockOption source, ItemStockOption target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.StockId = source.StockId;
            target.ItemId = source.ItemId;
            target.StockLabel = source.StockLabel;
            target.FileSource = source.FileSource;
            target.FileName = source.FileName;
            target.OptionSequence = source.OptionSequence;
        }

        #endregion
    }
}
