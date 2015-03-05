using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item Image mapper
    /// </summary>
    public static class ItemImageMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemImage source, ItemImage target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ProductImageId = source.ProductImageId;
            target.ItemId = source.ItemId;
            target.FileSource = source.FileSource;
            target.FileName = source.FileName;
        }

        #endregion
    }
}
