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
    
    public partial class StockItemHistory
    {
        public int ItemHId { get; set; }
        public Nullable<System.DateTime> ChangeDate { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<double> OldPackPrice { get; set; }
        public Nullable<double> NewPackPrice { get; set; }
        public int GRNId { get; set; }
        public Nullable<int> ItemId { get; set; }
    }
}
