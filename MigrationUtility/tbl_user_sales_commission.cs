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
    
    public partial class tbl_user_sales_commission
    {
        public int UserCommissionID { get; set; }
        public Nullable<int> SalesCommissionTypeID { get; set; }
        public Nullable<int> CommissionLookUpID { get; set; }
        public Nullable<int> SystemUserID { get; set; }
        public Nullable<double> FlatValue { get; set; }
        public string FlatDescription { get; set; }
        public Nullable<System.DateTime> LastUpdatedDate { get; set; }
        public Nullable<int> LastUpdatedBy { get; set; }
    }
}