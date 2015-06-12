using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Field Variable Domain Model
    /// </summary>
    public class FieldVariable
    {
        public long VariableId { get; set; }
        public string VariableName { get; set; }
        public string RefTableName { get; set; }
        public string CriteriaFieldName { get; set; }
        public long? VariableSectionId { get; set; }
        public string VariableTag { get; set; }
        public int? SortOrder { get; set; }
        public string KeyField { get; set; }
        public int? VariableType { get; set; }
        public int? Scope { get; set; }
        public string WaterMark { get; set; }
        public string DefaultValue { get; set; }
        public string InputMask { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }
        public bool? IsSystem { get; set; }
        public string VariableTitle { get; set; }
        public virtual Company Company { get; set; }
        public virtual ICollection<VariableOption> VariableOptions { get; set; }
        public virtual ICollection<ScopeVariable> ScopeVariables { get; set; }
        public virtual ICollection<SmartFormDetail> SmartFormDetails { get; set; }
        public virtual ICollection<TemplateVariable> TemplateVariables { get; set; }
        public virtual ICollection<VariableExtension> VariableExtensions { get; set; }

        [NotMapped]
        public long? FakeIdVariableId { get; set; }
    }
}
