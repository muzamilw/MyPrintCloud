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
    
    public partial class tbl_UserStickyNotes
    {
        public int NoteID { get; set; }
        public string NoteDescription { get; set; }
        public Nullable<int> UserID { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<System.DateTime> ModifyDate { get; set; }
    }
}
