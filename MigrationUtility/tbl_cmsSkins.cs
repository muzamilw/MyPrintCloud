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
    
    public partial class tbl_cmsSkins
    {
        public tbl_cmsSkins()
        {
            this.tbl_cmsColorPalletes = new HashSet<tbl_cmsColorPalletes>();
            this.tbl_cmsDefaultSettings = new HashSet<tbl_cmsDefaultSettings>();
        }
    
        public int SkinID { get; set; }
        public string SkinName { get; set; }
        public string ActualName { get; set; }
        public Nullable<int> Height { get; set; }
        public Nullable<int> Width { get; set; }
        public string JsFile1Path { get; set; }
        public string JsFile2Path { get; set; }
        public string Jsfile3Path { get; set; }
        public string Jsfile4Path { get; set; }
    
        public virtual ICollection<tbl_cmsColorPalletes> tbl_cmsColorPalletes { get; set; }
        public virtual ICollection<tbl_cmsDefaultSettings> tbl_cmsDefaultSettings { get; set; }
    }
}