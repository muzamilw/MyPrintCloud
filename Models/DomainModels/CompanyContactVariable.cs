using System.ComponentModel.DataAnnotations.Schema;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Company Contact Variable Domain Model
    /// </summary>
    public class CompanyContactVariable
    {
        public long ContactVariableId { get; set; }
        public long ContactId { get; set; }
        public long VariableId { get; set; }
        public string Value { get; set; }
        public virtual FieldVariable FieldVariable { get; set; }
        public virtual CompanyContact CompanyContact { get; set; }
        [NotMapped]
        public long? FakeVariableId { get; set; }
    }
}
