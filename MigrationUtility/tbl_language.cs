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
    
    public partial class tbl_language
    {
        public tbl_language()
        {
            this.tbl_general_settings = new HashSet<tbl_general_settings>();
        }
    
        public int LanguageId { get; set; }
        public string FriendlyName { get; set; }
        public string uiCulture { get; set; }
        public string culture { get; set; }
    
        public virtual ICollection<tbl_general_settings> tbl_general_settings { get; set; }
    }
}
