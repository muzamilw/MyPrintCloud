using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Product Category Item mapper
    /// </summary>
    public static class ProductCategoryItemMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ProductCategoryItem source, ProductCategoryItem target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.CategoryId = source.CategoryId;
            target.ItemId = source.ItemId;
        }

        #endregion
    }
}
