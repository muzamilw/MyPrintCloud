using System;
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
            return new ItemAttachment
            {
                ItemAttachmentId = source.ItemAttachmentId,
                FileTitle = source.FileTitle,
                CompanyId = source.CompanyId,
                FileName = source.FileName,
                FolderPath = !string.IsNullOrEmpty(source.FolderPath) ? source.FolderPath + "?" + DateTime.Now.ToString() : string.Empty,
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
                FileName = source.FileName,
                FolderPath = source.FolderPath
            };
        }
    }
}