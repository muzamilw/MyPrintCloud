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
    
    public partial class VariableOption
    {
        public long VariableOptionId { get; set; }
        public Nullable<long> VariableId { get; set; }
        public string Value { get; set; }
        public Nullable<int> SortOrder { get; set; }
    
        public virtual FieldVariable FieldVariable { get; set; }
    }
}
