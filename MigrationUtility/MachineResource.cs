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
    
    public partial class MachineResource
    {
        public long Id { get; set; }
        public Nullable<int> MachineId { get; set; }
        public Nullable<System.Guid> ResourceId { get; set; }
    
        public virtual Machine Machine { get; set; }
    }
}
