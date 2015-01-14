namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Company Variable Icon Domain Model
    /// </summary>
    public class CompanyVariableIcon
    {
        public long VariableIconId { get; set; }
        public long? VariableId { get; set; }
        public string Icon { get; set; }
        public long? ContactCompanyId { get; set; }
    }
}
