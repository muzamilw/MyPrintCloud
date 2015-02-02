using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Product Category Item Domain Model
    /// </summary>
    public class ProductCategoryItem
    {
        public long ProductCategoryItemId { get; set; }
        public long? CategoryId { get; set; }
        public long? ItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual ProductCategory ProductCategory { get; set; }

        #region Public

        /// <summary>
        /// Creates Copy of Entity
        /// </summary>
        public void Clone(ProductCategoryItem target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ProductCategoryItemClone_InvalidItem, "target");
            }

            target.CategoryId = CategoryId;
        }

        #endregion
    }
}
