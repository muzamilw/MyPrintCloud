using System;
using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Template Api Model
    /// </summary>
    public class Template
    {
        public long ProductId { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public long? ProductCategoryId { get; set; }
        public string LowResPdfTemplates { get; set; }
        public string BackgroundArtwork { get; set; }
        public string Side2LowResPdfTemplates { get; set; }
        public string Side2BackgroundArtwork { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public bool? IsDisabled { get; set; }
        public int? PTempId { get; set; }
        public bool IsDoubleSide { get; set; }
        public bool IsUsePdfFile { get; set; }
        public double? PdfTemplateWidth { get; set; }
        public double? PdfTemplateHeight { get; set; }
        public bool? IsUseBackGroundColor { get; set; }
        public int? BgR { get; set; }
        public int? BgG { get; set; }
        public int? BgB { get; set; }
        public bool? IsUseSide2BackGroundColor { get; set; }
        public int? Side2BgR { get; set; }
        public int? Side2BgG { get; set; }
        public int? Side2BgB { get; set; }
        public double? CuttingMargin { get; set; }
        public int? MultiPageCount { get; set; }
        public int? Orientation { get; set; }
        public string MatchingSetTheme { get; set; }
        public int? BaseColorId { get; set; }
        public int? SubmittedBy { get; set; }
        public string SubmittedByName { get; set; }
        public DateTime? SubmitDate { get; set; }
        public int? Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public int? UserRating { get; set; }
        public int? UsedCount { get; set; }
        public int? MpcRating { get; set; }
        public string RejectionReason { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string TempString { get; set; }
        public int? MatchingSetId { get; set; }
        public string FullView { get; set; }
        public string SlThumbnail { get; set; }
        public string SuperView { get; set; }
        public string ColorHex { get; set; }
        public int? TemplateOwner { get; set; }
        public string TemplateOwnerName { get; set; }
        public bool? IsPrivate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool? IsCorporateEditable { get; set; }
        public int? TemplateType { get; set; }
        public bool? IsWatermarkText { get; set; }
        public bool? IsSpotTemplate { get; set; }
        public bool? IsCreatedManual { get; set; }
        public bool? IsEditorChoice { get; set; }
        public IEnumerable<TemplatePage> TemplatePages { get; set; }

        /// <summary>
        /// File in Base64
        /// </summary>
        public string FileSource { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        public string FileName { get; set; }



        public bool? IsAllowCustomSize { get; set; }
        /// <summary>
        /// File Bytes Original
        /// </summary>
        public byte[] FileOriginalBytes { get; set; }

        /// <summary>
        /// File Original Source
        /// </summary>
        public string FileOriginalSource
        {
            get
            {
                if (FileOriginalBytes == null)
                {
                    return string.Empty;
                }

                string base64 = Convert.ToBase64String(FileOriginalBytes);
                return string.Format("data:{0};base64,{1}", "image/jpg", base64);
            }
        }
    }
}