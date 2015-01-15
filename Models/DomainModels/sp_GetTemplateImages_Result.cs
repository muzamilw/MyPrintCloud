namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Get Template Images Domain Model
    /// </summary>
    public class sp_GetTemplateImages_Result
    {
        public long? RowNum { get; set; }
        public int? ID { get; set; }
        public int? ProductID { get; set; }
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
        public int? ContactCompanyID { get; set; }
        public int? ContactID { get; set; }
    }
}
