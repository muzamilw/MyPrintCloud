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
    
    public partial class tbl_itemsection_costcentre_groups
    {
        public int ID { get; set; }
        public int ItemSectionID { get; set; }
        public Nullable<int> CostCentreGroupID { get; set; }
    
        public virtual tbl_item_sections tbl_item_sections { get; set; }
    }
}