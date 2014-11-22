using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public partial class ProductCategory
    {
        public int ProductCategoryId { get; set; }
        public string CategoryName { get; set; }
        public string ContentType { get; set; }
        public string Description1 { get; set; }
        public string Description2 { get; set; }
        public int LockedBy { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<int> ParentCategoryId { get; set; }
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
        public Nullable<int> CategoryTypeId { get; set; }
        public Nullable<int> RegionId { get; set; }
        public Nullable<decimal> ZoomFactor { get; set; }
        public Nullable<decimal> ScaleFactor { get; set; }
        public Nullable<bool> isShelfProductCategory { get; set; }
        public string MetaKeywords { get; set; }
        public string MetaDescription { get; set; }
        public string MetaTitle { get; set; }
        public Nullable<long> OrganisationId { get; set; }

        public virtual Company Company { get; set; }
    }
}
