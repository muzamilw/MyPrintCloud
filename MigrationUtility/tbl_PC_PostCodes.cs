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
    
    public partial class tbl_PC_PostCodes
    {
        public tbl_PC_PostCodes()
        {
            this.tbl_PC_PostCodesBrokers = new HashSet<tbl_PC_PostCodesBrokers>();
        }
    
        public int OutPostCodeID { get; set; }
        public string OutPostCode { get; set; }
        public string City { get; set; }
        public string County { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    
        public virtual ICollection<tbl_PC_PostCodesBrokers> tbl_PC_PostCodesBrokers { get; set; }
    }
}