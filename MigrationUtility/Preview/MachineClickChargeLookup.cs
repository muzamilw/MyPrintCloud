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
    
    public partial class MachineClickChargeLookup
    {
        public long Id { get; set; }
        public long MethodId { get; set; }
        public Nullable<double> SheetCost { get; set; }
        public Nullable<int> Sheets { get; set; }
        public Nullable<double> SheetPrice { get; set; }
        public Nullable<double> TimePerHour { get; set; }
    
        public virtual LookupMethod LookupMethod { get; set; }
    }
}
