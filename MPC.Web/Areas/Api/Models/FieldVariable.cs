using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Field Variable Api Model
    /// </summary>
    public class FieldVariable
    {
        public long VariableId { get; set; }
        public long? FakeIdVariableId { get; set; }
        public string VariableName { get; set; }
        public string VariableTag { get; set; }
        public int? VariableType { get; set; }
        public int? Scope { get; set; }
        public string WaterMark { get; set; }
        public string DefaultValue { get; set; }
        public string InputMask { get; set; }
        public long? CompanyId { get; set; }
        public string VariableTitle { get; set; }
        public string TypeName { get; set; }
        public string ScopeName { get; set; }

        public List<VariableOption> VariableOptions { get; set; }
        public List<VariableExtension> VariableExtensions { get; set; }

    }
}