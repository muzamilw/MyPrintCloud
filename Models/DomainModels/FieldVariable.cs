﻿using System.Collections.Generic;

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
        public int? VariableSectionId { get; set; }
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
        public virtual ICollection<VariableOption> VariableOptions { get; set; }
    }
}
