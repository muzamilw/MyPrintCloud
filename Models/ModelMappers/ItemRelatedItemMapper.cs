using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item Related Item mapper
    /// </summary>
    public static class ItemRelatedItemMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemRelatedItem source, ItemRelatedItem target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.RelatedItemId = source.RelatedItemId;
            target.ItemId = source.ItemId;
        }

        #endregion
    }
}
