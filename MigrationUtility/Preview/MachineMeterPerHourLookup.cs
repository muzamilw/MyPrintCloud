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
    
    public partial class MachineMeterPerHourLookup
    {
        public long Id { get; set; }
        public Nullable<long> MethodId { get; set; }
        public Nullable<long> SheetsQty1 { get; set; }
        public Nullable<long> SheetsQty2 { get; set; }
        public Nullable<long> SheetsQty3 { get; set; }
        public Nullable<long> SheetsQty4 { get; set; }
        public Nullable<long> SheetsQty5 { get; set; }
        public Nullable<long> SheetWeight1 { get; set; }
        public Nullable<long> speedqty11 { get; set; }
        public Nullable<long> speedqty12 { get; set; }
        public Nullable<long> speedqty13 { get; set; }
        public Nullable<long> speedqty14 { get; set; }
        public Nullable<long> speedqty15 { get; set; }
        public Nullable<long> SheetWeight2 { get; set; }
        public Nullable<long> speedqty21 { get; set; }
        public Nullable<long> speedqty22 { get; set; }
        public Nullable<long> speedqty23 { get; set; }
        public Nullable<long> speedqty24 { get; set; }
        public Nullable<long> speedqty25 { get; set; }
        public Nullable<long> SheetWeight3 { get; set; }
        public Nullable<long> speedqty31 { get; set; }
        public Nullable<long> speedqty32 { get; set; }
        public Nullable<long> speedqty33 { get; set; }
        public Nullable<long> speedqty34 { get; set; }
        public Nullable<long> speedqty35 { get; set; }
        public Nullable<double> hourlyCost { get; set; }
        public double hourlyPrice { get; set; }
    
        public virtual LookupMethod LookupMethod { get; set; }
    }
}
