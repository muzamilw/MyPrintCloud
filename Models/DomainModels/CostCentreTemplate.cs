namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cost Center Template Domain Model
    /// </summary>
    public class CostCentreTemplate
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Header { get; set; }
        public string Footer { get; set; }
        public string Middle { get; set; }
        public int Type { get; set; }
    }
}
