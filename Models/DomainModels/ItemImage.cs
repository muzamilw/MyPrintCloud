using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Windows.Forms.Design;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Item Image Domain Model
    /// </summary>
    public class ItemImage
    {
        public int ProductImageId { get; set; }
        public long? ItemId { get; set; }
        public string ImageTitle { get; set; }
        public string ImageURL { get; set; }
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public DateTime? UploadDate { get; set; }

        public virtual Item Item { get; set; }

        #region Additional Properties

        /// <summary>
        /// File in Base64
        /// </summary>
        [NotMapped]
        public string FileSource { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        [NotMapped]
        public string FileName { get; set; }

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


        #region Public

        /// <summary>
        /// Makes a copy of Item Image
        /// </summary>
        public void Clone(ItemImage target)
        {
            if (target == null)
            {
                throw new ArgumentException(LanguageResources.ItemClone_InvalidItem, "target");
            }

            target.ImageName = ImageName;
            target.ImageTitle = ImageTitle;
            target.ImageURL = ImageURL;
            target.ImageType = ImageType;
            target.UploadDate = UploadDate;
        }

        #endregion
    }
}
