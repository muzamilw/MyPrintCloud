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
    
    public partial class ItemSectionCostCentreGroup
    {
        public int Id { get; set; }
        public long ItemSectionId { get; set; }
        public Nullable<int> CostCentreGroupId { get; set; }
    
        public virtual ItemSection ItemSection { get; set; }
    }
}