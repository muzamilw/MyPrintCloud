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
    
    public partial class Roleright
    {
        public int RRId { get; set; }
        public int RoleId { get; set; }
        public int RightId { get; set; }
    
        public virtual AccessRight AccessRight { get; set; }
        public virtual Role Role { get; set; }
    }
}
