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
    
    public partial class ScheduledTimeActivity
    {
        public int ScheduledTimeActivityId { get; set; }
        public Nullable<System.DateTime> CreationDateTime { get; set; }
        public Nullable<System.DateTime> StartTime { get; set; }
        public Nullable<System.DateTime> EndTime { get; set; }
        public int UserLockedBy { get; set; }
        public Nullable<System.DateTime> LockDateTime { get; set; }
        public Nullable<System.DateTime> DeliveryTime { get; set; }
        public int ActivityStatusID { get; set; }
        public int JobID { get; set; }
        public int CostCenterId { get; set; }
        public int JobBindId { get; set; }
        public Nullable<short> IsLocked { get; set; }
        public Nullable<short> IsCompleted { get; set; }
        public Nullable<double> OleColorCode { get; set; }
        public Nullable<short> IsInEditing { get; set; }
        public int Mode { get; set; }
        public int LinkedScheduledTimeActivityId { get; set; }
        public int LockedBy { get; set; }
    }
}
