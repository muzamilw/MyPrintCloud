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
    
    public partial class GroupDetail
    {
        public int GroupDetailId { get; set; }
        public int ContactId { get; set; }
        public short IsCustomerContact { get; set; }
        public int GroupId { get; set; }
    
        public virtual Group Group { get; set; }
    }
}
