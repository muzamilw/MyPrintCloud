using System;
using System.Globalization;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ItemAttachmentMapper
    {
        /// <summary>
        /// Server to Client Mapper
        /// </summary>
        public static ItemAttachment CreateFrom(this MPC.Models.DomainModels.ItemAttachment source)
        {
            string filePath = !string.IsNullOrEmpty(source.FolderPath) ? source.FolderPath : string.Empty;
            string fileType = !string.IsNullOrEmpty(source.FileType) ? source.FileType : string.Empty;
            string fileName = !string.IsNullOrEmpty(source.FileName) ? source.FileName + fileType + "?" +
                DateTime.Now.ToString(CultureInfo.InvariantCulture) : string.Empty;
            filePath += "/" + fileName;
            return new ItemAttachment
            {
                ItemAttachmentId = source.ItemAttachmentId,
                FileTitle = source.FileTitle,
                CompanyId = source.CompanyId,
                UploadDate = source.UploadDate,
                FileName = source.FileName,
                FileType = source.FileType,
                ItemId = source.ItemId,
                ContactId = source.ContactId,
                Comments = source.Comments,
                FolderPath = filePath,
                Parent = source.Parent,
                Type = source.Type
            };
        }

        /// <summary>
        /// Client Server Mapper
        /// </summary>
        public static MPC.Models.DomainModels.ItemAttachment CreateFrom(this ItemAttachment source)
        {
            return new MPC.Models.DomainModels.ItemAttachment
            {
                ItemAttachmentId = source.ItemAttachmentId,
                FileTitle = source.FileTitle,
                CompanyId = source.CompanyId,
                ItemId = source.ItemId,
                FileName = source.FileName,
                ContactId = source.ContactId,
                FileSource = source.FolderPath,
                Parent = source.Parent,
                Type = source.Type,
                Comments = source.Comments,


            };
        }
        /// <summary>
        /// Server to Client Mapper
        /// </summary>
        public static ItemAttachmentForLiveJobs CreateFromForLiveJobs(this MPC.Models.DomainModels.ItemAttachment source)
        {

            return new ItemAttachmentForLiveJobs
            {
                FileType = source.FileType,
            };
        }
    }
}