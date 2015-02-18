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
        public virtual CompanyContact CompanyContact { get; set; }
    }
}
