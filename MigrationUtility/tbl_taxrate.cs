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
    
    public partial class tbl_taxrate
    {
        public int TaxID { get; set; }
        public string TaxCode { get; set; }
        public string TaxName { get; set; }
        public Nullable<int> SourceStateID { get; set; }
        public Nullable<int> DestinationStateID { get; set; }
        public Nullable<double> Tax1 { get; set; }
        public Nullable<double> Tax2 { get; set; }
        public Nullable<double> Tax3 { get; set; }
        public Nullable<bool> IsFixed { get; set; }
    }
}