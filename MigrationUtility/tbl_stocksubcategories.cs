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
    
    public partial class tbl_stocksubcategories
    {
        public tbl_stocksubcategories()
        {
            this.tbl_stockitems = new HashSet<tbl_stockitems>();
        }
    
        public int SubCategoryID { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Fixed { get; set; }
        public int CategoryID { get; set; }
    
        public virtual ICollection<tbl_stockitems> tbl_stockitems { get; set; }
        public virtual tbl_stockcategories tbl_stockcategories { get; set; }
    }
}
