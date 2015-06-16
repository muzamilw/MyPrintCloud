using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Company Contact Variable API Model
    /// </summary>
    public class ScopeVariable
    {
        public long ScopeVariableId { get; set; }
        public long Id { get; set; }
        public long VariableId { get; set; }
        public string Value { get; set; }
        public int? Type { get; set; }
        public string Title { get; set; }
        public string WaterMark { get; set; }
        public int? Scope { get; set; }
        public long? FakeVariableId { get; set; }

        public List<VariableOption> VariableOptions { get; set; }

    }
}