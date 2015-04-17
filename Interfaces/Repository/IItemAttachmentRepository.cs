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

        ItemAttachment PopulueTblItemAttachment(long itemID, long customerID, long? contactId, string fileTitle, string fileName, UploadFileTypes type, string fileExtention, string folderPath);
    }
}
