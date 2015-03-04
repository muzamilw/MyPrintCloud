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
    
    public partial class StockCategory
    {
        public StockCategory()
        {
            this.StockSubCategories = new HashSet<StockSubCategory>();
            this.StockItems = new HashSet<StockItem>();
        }
    
        public long CategoryId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short @fixed { get; set; }
        public short ItemWeight { get; set; }
        public short ItemColour { get; set; }
        public short ItemSizeCustom { get; set; }
        public short ItemPaperSize { get; set; }
        public short ItemCoatedType { get; set; }
        public Nullable<short> ItemCoated { get; set; }
        public short ItemExposure { get; set; }
        public short ItemCharge { get; set; }
        public short recLock { get; set; }
        public Nullable<int> TaxId { get; set; }
        public Nullable<short> Flag1 { get; set; }
        public Nullable<short> Flag2 { get; set; }
        public Nullable<short> Flag3 { get; set; }
        public Nullable<short> Flag4 { get; set; }
        public long OrganisationId { get; set; }
    
        public virtual ICollection<StockSubCategory> StockSubCategories { get; set; }
        public virtual ICollection<StockItem> StockItems { get; set; }
    }
}