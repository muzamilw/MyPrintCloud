using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item Section mapper
    /// </summary>
    public static class ItemSectionMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemSection source, ItemSection target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ItemSectionId = source.ItemSectionId;
            target.SectionNo = source.SectionNo;
            target.SectionName = source.SectionName;
            target.ItemId = source.ItemId;
            target.PressId = source.PressId;
            target.StockItemID1 = source.StockItemID1;
            target.SectionSizeId = source.SectionSizeId;
            target.ItemSizeId = source.ItemSizeId;
            target.SectionSizeHeight = source.SectionSizeHeight;
            target.SectionSizeWidth = source.SectionSizeWidth;
            target.ItemSizeHeight = source.ItemSizeHeight;
            target.ItemSizeWidth = source.ItemSizeWidth;
            target.IsItemSizeCustom = source.IsItemSizeCustom;
            target.IsSectionSizeCustom = source.IsSectionSizeCustom;
        }

        #endregion
    }
}
