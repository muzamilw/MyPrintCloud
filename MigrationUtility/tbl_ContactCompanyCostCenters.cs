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
    
    public partial class tbl_ContactCompanyCostCenters
    {
        public int CompanyCostCenterID { get; set; }
        public Nullable<int> ContactCompanyID { get; set; }
        public Nullable<int> CostCentreID { get; set; }
        public Nullable<double> BrokerMarkup { get; set; }
        public Nullable<double> ContactMarkup { get; set; }
        public Nullable<bool> isDisplayToUser { get; set; }
    }
}
