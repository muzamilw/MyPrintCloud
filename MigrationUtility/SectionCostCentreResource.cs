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
    
    public partial class SectionCostCentreResource
    {
        public int SectionCostCentreResourceId { get; set; }
        public Nullable<int> SectionCostcentreId { get; set; }
        public Nullable<System.Guid> ResourceId { get; set; }
        public Nullable<int> ResourceTime { get; set; }
        public Nullable<short> IsScheduleable { get; set; }
        public Nullable<short> IsScheduled { get; set; }
    
        public virtual SectionCostcentre SectionCostcentre { get; set; }
    }
}
