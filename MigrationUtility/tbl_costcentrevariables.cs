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
    
    public partial class tbl_costcentrevariables
    {
        public int VarID { get; set; }
        public string Name { get; set; }
        public string RefTableName { get; set; }
        public string RefFieldName { get; set; }
        public string CriteriaFieldName { get; set; }
        public string Criteria { get; set; }
        public int CategoryID { get; set; }
        public string IsCriteriaUsed { get; set; }
        public short Type { get; set; }
        public Nullable<int> PropertyType { get; set; }
        public string VariableDescription { get; set; }
        public Nullable<double> VariableValue { get; set; }
        public int SystemSiteID { get; set; }
    }
}
