//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MigrationUtility.Preview
{
    using System;
    using System.Collections.Generic;
    
    public partial class CmsSkinPageWidget
    {
        public CmsSkinPageWidget()
        {
            this.CmsSkinPageWidgetParams = new HashSet<CmsSkinPageWidgetParam>();
        }
    
        public long PageWidgetId { get; set; }
        public Nullable<long> PageId { get; set; }
        public Nullable<long> WidgetId { get; set; }
        public Nullable<long> SkinId { get; set; }
        public Nullable<short> Sequence { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> OrganisationId { get; set; }
    
        public virtual CmsPage CmsPage { get; set; }
        public virtual Company Company { get; set; }
        public virtual Organisation Organisation { get; set; }
        public virtual Widget Widget { get; set; }
        public virtual ICollection<CmsSkinPageWidgetParam> CmsSkinPageWidgetParams { get; set; }
        public virtual tbl_cmsSkins tbl_cmsSkins { get; set; }
    }
}
