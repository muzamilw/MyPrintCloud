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
    
    public partial class tbl_costcentrequestions
    {
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public Nullable<short> Type { get; set; }
        public string DefaultAnswer { get; set; }
        public int CompanyID { get; set; }
        public int SystemSiteID { get; set; }
    }
}
