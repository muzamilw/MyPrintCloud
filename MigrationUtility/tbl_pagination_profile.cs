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
    
    public partial class tbl_pagination_profile
    {
        public int ID { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public Nullable<int> Priority { get; set; }
        public Nullable<int> Pages { get; set; }
        public Nullable<int> PaperSizeID { get; set; }
        public Nullable<int> LookupMethodID { get; set; }
        public Nullable<int> Orientation { get; set; }
        public Nullable<int> FinishStyleID { get; set; }
        public Nullable<double> MinHeight { get; set; }
        public Nullable<double> Minwidth { get; set; }
        public Nullable<double> Maxheight { get; set; }
        public Nullable<double> MaxWidth { get; set; }
        public Nullable<double> MinWeight { get; set; }
        public Nullable<double> MaxWeight { get; set; }
        public Nullable<int> MaxNoOfColors { get; set; }
        public string GrainDirection { get; set; }
        public Nullable<int> NumberUp { get; set; }
        public Nullable<int> NoOfDifferentTypes { get; set; }
        public Nullable<int> LockedBy { get; set; }
        public int CompanyID { get; set; }
        public Nullable<int> FlagID { get; set; }
        public int SystemSiteID { get; set; }
    }
}
