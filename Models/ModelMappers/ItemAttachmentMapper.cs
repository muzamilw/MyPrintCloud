﻿using MPC.Models.DomainModels;
namespace MPC.Models.ModelMappers
{
    using System;

    /// <summary>
    /// Item Attachment mapper
    /// </summary>
    public static class ItemAttachmentMapper
    {
        #region Public

        /// <summary>
        ///  Copy from source entity to the target
        /// </summary>
        public static void UpdateTo(this ItemAttachment source, ItemAttachment target)
        {
            if (source == null)
            {
                throw new ArgumentNullException("source");
            }
            if (target == null)
            {
                throw new ArgumentNullException("target");
            }

            target.ItemAttachmentId = source.ItemAttachmentId;
            target.ItemId = source.ItemId;
            target.FileName = source.FileName;
            target.FileSource = source.FileSource;
        }

        #endregion
    }
}
