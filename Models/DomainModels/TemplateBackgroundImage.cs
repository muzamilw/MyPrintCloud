using System.Collections;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Template Background Image Domain Model
    /// </summary>
    public class TemplateBackgroundImage
    {
        public long Id { get; set; }
        public long? ProductId { get; set; }
        public string ImageName { get; set; }
        public string Name { get; set; }
        public bool? flgPhotobook { get; set; }
        public bool? flgCover { get; set; }
        public string BackgroundImageAbsolutePath { get; set; }
        public string BackgroundImageRelativePath { get; set; }
        public int? ImageType { get; set; }
        public int? ImageWidth { get; set; }
        public int? ImageHeight { get; set; }
        public string ImageTitle { get; set; }
        public string ImageDescription { get; set; }
        public string ImageKeywords { get; set; }
        public int? UploadedFrom { get; set; }
        public long? ContactCompanyId { get; set; }
        public long? ContactId { get; set; }
        public bool? hasClippingPath { get; set; }
        public string clippingFileName { get; set; }

        public virtual Template Template { get; set; }

        public virtual ICollection<ImagePermission> ImagePermissions { get; set; }
    }
}
