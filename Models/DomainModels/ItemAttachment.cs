using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
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
        public string ContentType { get; set; }
        public Nullable<int> Parent { get; set; }
        public Nullable<System.DateTime> ApproveDate { get; set; }
        public string ImageFileType { get; set; }
        public virtual Item Item { get; set; }

        #region Additional Properties

        /// <summary>
        /// File in Base64
        /// </summary>
        [NotMapped]
        public string FileSource { get; set; }

        /// <summary>
        /// File Source Bytes - byte[] representation of Base64 string FileSource
        /// </summary>
        [NotMapped]
        public byte[] FileSourceBytes
        {
            get
            {
                if (string.IsNullOrEmpty(FileSource))
                {
                    return null;
                }

                int firtsAppearingCommaIndex = FileSource.IndexOf(',');

                if (firtsAppearingCommaIndex < 0)
                {
                    return null;
                }

                if (FileSource.Length < firtsAppearingCommaIndex + 1)
                {
                    return null;
                }

                string sourceSubString = FileSource.Substring(firtsAppearingCommaIndex + 1);

                try
                {
                    return Convert.FromBase64String(sourceSubString.Trim('\0'));
                }
                catch (FormatException)
                {
                    return null;
                }
            }
        }

        #endregion

    }
}
