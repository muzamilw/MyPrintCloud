namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Template Background Image Domain Model
    /// </summary>
    public class TemplateBackgroundImage
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
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
        public int? ContactCompanyId { get; set; }
        public int? ContactId { get; set; }

        public virtual Template Template { get; set; }
    }
}
