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
    
    public partial class tbl_itemImages
    {
        public int ProductImageID { get; set; }
        public Nullable<int> ItemID { get; set; }
        public string ImageTitle { get; set; }
        public string ImageURL { get; set; }
        public string ImageType { get; set; }
        public string ImageName { get; set; }
        public Nullable<System.DateTime> UploadDate { get; set; }
    
        public virtual tbl_items tbl_items { get; set; }
    }
}
