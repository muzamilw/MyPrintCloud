using System;

namespace MPC.Models.DomainModels
{
    public class ItemRelatedItem
    {
        public long? ItemId { get; set; }
        public int Id { get; set; }
        public long? RelatedItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Item RelatedItem { get; set; }

        #region Public

        /// <summary>
        /// Creates Copy of Entity
        /// </summary>
        public void Clone(ItemRelatedItem target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ProductCategoryItemClone_InvalidItem, "target");
            }

            target.RelatedItemId = RelatedItemId;
        }

        #endregion
    }
}
