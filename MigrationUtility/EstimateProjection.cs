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
    
    public partial class EstimateProjection
    {
        public int ProjectionId { get; set; }
        public Nullable<int> ContactCompanyId { get; set; }
        public Nullable<double> Amount { get; set; }
        public Nullable<int> SuccessChanceId { get; set; }
        public Nullable<int> SalesPerson { get; set; }
        public Nullable<System.DateTime> EstimateDate { get; set; }
        public Nullable<bool> IsProjectionAlarm { get; set; }
        public Nullable<System.DateTime> AlarmDate { get; set; }
        public Nullable<System.DateTime> AlarmTime { get; set; }
        public string Notes { get; set; }
        public Nullable<short> IsIncludedInPipeLine { get; set; }
        public Nullable<int> SourceId { get; set; }
        public Nullable<int> ProductId { get; set; }
        public Nullable<short> IsLocked { get; set; }
        public Nullable<int> LockedBy { get; set; }
    }
}
