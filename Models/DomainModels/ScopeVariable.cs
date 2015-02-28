using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Scope Variable Domain Model
    /// </summary>
    public class ScopeVariable
    {
        public long ScopeVariableId { get; set; }
        public long Id { get; set; }
        public long VariableId { get; set; }
        public string Value { get; set; }
        public int? Scope { get; set; }
        public virtual FieldVariable FieldVariable { get; set; }

        [NotMapped]
        public long? FakeVariableId { get; set; }
    }
}
