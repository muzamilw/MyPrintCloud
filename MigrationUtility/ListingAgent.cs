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
    
    public partial class ListingAgent
    {
        public long AgentId { get; set; }
        public Nullable<long> MemberId { get; set; }
        public Nullable<int> AgentOrder { get; set; }
        public Nullable<long> ListingId { get; set; }
        public string UserRef { get; set; }
        public string Name { get; set; }
        public Nullable<bool> Admin { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Mobile { get; set; }
        public Nullable<bool> Deleted { get; set; }
    }
}
