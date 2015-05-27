using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;


namespace MPC.Interfaces.Repository
{
    public interface IItemAttachmentRepository : IBaseRepository<ItemAttachment, long>
    {
        List<ItemAttachment> GetArtworkAttachments(long ItemId);
        /// <summary>
        /// save attachment list in data base after uploading file
        /// </summary>
        /// <param name="attachmentList"></param>
        List<ItemAttachment> SaveArtworkAttachments(List<ItemAttachment> attachmentList);
        List<ItemAttachment> GetItemAttactchments(long itemID);

        List<ArtWorkAttatchment> GetItemAttactchmentsForRegenerateTemplateAttachments(long itemID, string fileExtionsion, UploadFileTypes uploadedFileType);

        /// <summary>
        /// gets the single attachment record
        /// </summary>
        /// <param name="AttachmentId"></param>
        /// <returns></returns>
        ItemAttachment GetArtworkAttachment(long AttachmentId);
        /// <summary>
        /// delete attachment 
        /// </summary>
        /// <param name="AttachmentId"></param>
        /// <returns></returns>
        void DeleteArtworkAttachment(ItemAttachment AttachmentRecord);

        /// <summary>
        /// Get Item Attachments By Ids
        /// </summary>
        List<ItemAttachment> GetItemAttachmentsByIds(List<long?> itemIds);
    }
}
