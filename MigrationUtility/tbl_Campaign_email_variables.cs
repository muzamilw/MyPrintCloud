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
    
    public partial class tbl_Campaign_email_variables
    {
        public int VariableID { get; set; }
        public string VariableName { get; set; }
        public string RefTableName { get; set; }
        public string RefFieldName { get; set; }
        public string CriteriaFieldName { get; set; }
        public string Description { get; set; }
        public Nullable<int> SectionID { get; set; }
        public string VariableTag { get; set; }
        public string Key { get; set; }
    }
}