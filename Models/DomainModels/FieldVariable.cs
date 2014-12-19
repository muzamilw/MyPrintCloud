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
    }
}
