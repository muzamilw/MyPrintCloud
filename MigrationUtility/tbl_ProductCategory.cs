//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_ProductCategory
    {
        public tbl_ProductCategory()
        {
            this.tbl_items = new HashSet<tbl_items>();
            this.Templates = new HashSet<Template>();
            this.tbl_ProductCategoryFoldLines = new HashSet<tbl_ProductCategoryFoldLines>();
        }
    
        public int ProductCategoryID { get; set; }
        public string CategoryName { get; set; }
        public string ContentType { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public int LockedBy { get; set; }
        public Nullable<int> ContactCompanyID { get; set; }
        public Nullable<int> ParentCategoryID { get; set; }
        public int DisplayOrder { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }
        public Nullable<bool> isEnabled { get; set; }
        public Nullable<bool> isMarketPlace { get; set; }
        public string TemplateDesignerMappedCategoryName { get; set; }
        public Nullable<bool> isArchived { get; set; }
        public Nullable<bool> isPublished { get; set; }
        public Nullable<double> TrimmedWidth { get; set; }
        public Nullable<double> TrimmedHeight { get; set; }
        public Nullable<bool> isColorImposition { get; set; }
        public Nullable<bool> isOrderImposition { get; set; }
        public Nullable<bool> isLinkToTemplates { get; set; }
        public Nullable<int> Sides { get; set; }
        public Nullable<bool> ApplySizeRestrictions { get; set; }
        public Nullable<bool> ApplyFoldLines { get; set; }
        public Nullable<double> WidthRestriction { get; set; }
        public Nullable<double> HeightRestriction { get; set; }
        public Nullable<int> CategoryTypeID { get; set; }
        public Nullable<int> RegionID { get; set; }
        public Nullable<decimal> ZoomFactor { get; set; }
        public Nullable<decimal> ScaleFactor { get; set; }
        public Nullable<bool> isShelfProductCategory { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
    
        public virtual tbl_contactcompanies tbl_contactcompanies { get; set; }
        public virtual ICollection<tbl_items> tbl_items { get; set; }
        public virtual ICollection<Template> Templates { get; set; }
        public virtual ICollection<tbl_ProductCategoryFoldLines> tbl_ProductCategoryFoldLines { get; set; }
    }
}
