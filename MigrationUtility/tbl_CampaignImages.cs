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
    
    public partial class tbl_CampaignImages
    {
        public int CampaignImageID { get; set; }
        public Nullable<int> CampaignID { get; set; }
        public string ImagePath { get; set; }
        public string ImageName { get; set; }
    
        public virtual tbl_campaigns tbl_campaigns { get; set; }
    }
}
