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
    
    public partial class StockCostAndPrice
    {
        public int CostPriceId { get; set; }
        public long ItemId { get; set; }
        public double CostPrice { get; set; }
        public double PackCostPrice { get; set; }
        public Nullable<System.DateTime> FromDate { get; set; }
        public Nullable<System.DateTime> ToDate { get; set; }
        public short CostOrPriceIdentifier { get; set; }
        public double ProcessingCharge { get; set; }
    
        public virtual StockItem StockItem { get; set; }
    }
}
