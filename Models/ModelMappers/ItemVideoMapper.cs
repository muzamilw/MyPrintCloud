using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item Video mapper
    /// </summary>
    public static class ItemVideoMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemVideo source, ItemVideo target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.VideoId = source.VideoId;
            target.ItemId = source.ItemId;
            target.Caption = source.Caption;
            target.VideoLink = source.VideoLink;
        }

        #endregion
    }
}
