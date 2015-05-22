namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Field Variable For Smart Form
    /// </summary>
    public class FieldVariableForSmartForm
    {
        public long VariableId { get; set; }
        public string VariableName { get; set; }
        public string ScopeName { get; set; }
        public string VariableTag { get; set; }
        public string TypeName { get; set; }
        public string DefaultValue { get; set; }
        public string VariableTitle { get; set; }
        public string WaterMark { get; set; }

        public int? Scope { get; set; }
        public int? Type { get; set; }
    }
}