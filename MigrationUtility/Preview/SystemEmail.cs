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
    
    public partial class SystemEmail
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string FFrom { get; set; }
        public string FromEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
        public string TextBody { get; set; }
        public Nullable<int> LockedBy { get; set; }
        public int SystemSiteId { get; set; }
    }
}