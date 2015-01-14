using System;
using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Template Domain Model
    /// </summary>
    public class Template
    {
        public long ProductId { get; set; }
        public string Code { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public string Thumbnail { get; set; }
        public string Image { get; set; }
        public bool? IsDisabled { get; set; }
        public double? PDFTemplateWidth { get; set; }
        public double? PDFTemplateHeight { get; set; }
        public double? CuttingMargin { get; set; }
        public int? MultiPageCount { get; set; }
        public int? Orientation { get; set; }
        public string MatchingSetTheme { get; set; }
        public int? BaseColorID { get; set; }
        public int? SubmittedBy { get; set; }
        public string SubmittedByName { get; set; }
        public DateTime? SubmitDate { get; set; }
        public int? Status { get; set; }
        public int? ApprovedBy { get; set; }
        public string ApprovedByName { get; set; }
        public int? UserRating { get; set; }
        public int? UsedCount { get; set; }
        public int? MPCRating { get; set; }
        public string RejectionReason { get; set; }
        public DateTime? ApprovalDate { get; set; }
        public string TempString { get; set; }
        public int? MatchingSetID { get; set; }
        public string FullView { get; set; }
        public string SLThumbnail { get; set; }
        public string SuperView { get; set; }
        public string ColorHex { get; set; }
        public int? TemplateOwner { get; set; }
        public string TemplateOwnerName { get; set; }
        public bool? IsPrivate { get; set; }
        public DateTime? ApprovedDate { get; set; }
        public bool? IsCorporateEditable { get; set; }
        public int? TemplateType { get; set; }
        public bool? isWatermarkText { get; set; }
        public bool? isSpotTemplate { get; set; }
        public bool? isCreatedManual { get; set; }
        public bool? isEditorChoice { get; set; }
        public long? ProductCategoryId { get; set; }
        public virtual ICollection<TemplatePage> TemplatePages { get; set; }
        public virtual ICollection<Item> Items { get; set; }
        public virtual ICollection<TemplateBackgroundImage> TemplateBackgroundImages { get; set; }
        public virtual ICollection<TemplateObject> TemplateObjects { get; set; }
        public virtual ICollection<TemplateColorStyle> TemplateColorStyles { get; set; }
        public virtual ICollection<TemplateFont> TemplateFonts { get; set; }
    }

}
