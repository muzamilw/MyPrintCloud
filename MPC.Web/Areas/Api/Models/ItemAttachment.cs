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
        public Nullable<short> isFromCustomer { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> ContactId { get; set; }
        public Nullable<int> SystemUserId { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
        public Nullable<System.DateTime> UploadTime { get; set; }
        public Nullable<int> Version { get; set; }
        public Nullable<long> ItemId { get; set; }
        public string Comments { get; set; }
        public string Type { get; set; }
        public Nullable<short> IsApproved { get; set; }
        public string FileName { get; set; }
        public string FolderPath { get; set; }
        public string FileType { get; set; }
        public string FileSourcePath { get; set; }
        

    }
}