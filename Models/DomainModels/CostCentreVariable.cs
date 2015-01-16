namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cost Centre Variable Domain Model
    /// </summary>
    public class CostCentreVariable
    {
        public int VarId { get; set; }
        public string Name { get; set; }
        public string RefTableName { get; set; }
        public string RefFieldName { get; set; }
        public string CriteriaFieldName { get; set; }
        public string Criteria { get; set; }
        public int CategoryId { get; set; }
        public string IsCriteriaUsed { get; set; }
        public short Type { get; set; }
        public int? PropertyType { get; set; }
        public string VariableDescription { get; set; }
        public double? VariableValue { get; set; }
        public int SystemSiteId { get; set; }
    }
}
