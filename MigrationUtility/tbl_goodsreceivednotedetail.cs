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
    
    public partial class tbl_goodsreceivednotedetail
    {
        public int GoodsReceivedDetailID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public Nullable<double> QtyReceived { get; set; }
        public Nullable<int> GoodsreceivedID { get; set; }
        public Nullable<double> Price { get; set; }
        public Nullable<int> PackQty { get; set; }
        public Nullable<double> TotalOrderedqty { get; set; }
        public string Details { get; set; }
        public string ItemCode { get; set; }
        public string Name { get; set; }
        public Nullable<double> TotalPrice { get; set; }
        public Nullable<int> TaxID { get; set; }
        public Nullable<double> NetTax { get; set; }
        public Nullable<double> Discount { get; set; }
        public Nullable<int> FreeItems { get; set; }
        public Nullable<int> DepartmentID { get; set; }
    
        public virtual tbl_goodsreceivednote tbl_goodsreceivednote { get; set; }
    }
}