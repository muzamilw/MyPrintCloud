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
    
    public partial class tbl_ContactCompanyItems
    {
        public int CompanyItemID { get; set; }
        public Nullable<int> ContactCompanyID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<double> BrokerMarkup { get; set; }
        public Nullable<double> ContactMarkup { get; set; }
        public Nullable<bool> isDisplayToUser { get; set; }
        public string ImagePath { get; set; }
        public string ThumbnailPath { get; set; }
        public string WebDescription { get; set; }
        public string ProductSpecification { get; set; }
        public string TipsAndHints { get; set; }
        public string HowToVideoContent { get; set; }
    }
}