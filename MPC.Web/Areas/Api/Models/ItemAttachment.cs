using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Attachment Web Model
    /// </summary>
    public class ItemAttachment
    {
        public long ItemAttachmentId { get; set; }
        public string FileTitle { get; set; }
        public short? isFromCustomer { get; set; }
        public long? CompanyId { get; set; }
        public long? ContactId { get; set; }
        public int? SystemUserId { get; set; }
        public DateTime? UploadDate { get; set; }
        public DateTime? UploadTime { get; set; }
        public int? Version { get; set; }
        public int? Parent { get; set; }
        public long? ItemId { get; set; }
        public string Comments { get; set; }
        public string Type { get; set; }
        public short? IsApproved { get; set; }
        public string FileName { get; set; }
        public string FolderPath { get; set; }
        public string FileType { get; set; }
        public string FileSourcePath { get; set; }
        

    }
}