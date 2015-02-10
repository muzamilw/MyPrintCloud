namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Cost Centre Choice Domain Model
    /// </summary>
    public class CostCenterChoice
    {
        public int CostCenterChoiceId { get; set; }
        public string ChoiceLabel { get; set; }
        public double? ChoiceValue { get; set; }
        public int? CostCenterId { get; set; }
        public int? CostCenterOption { get; set; }
    }
}
