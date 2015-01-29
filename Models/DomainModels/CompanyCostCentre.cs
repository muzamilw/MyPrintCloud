namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Company Cost Centre Domain Model
    /// </summary>
    public class CompanyCostCentre
    {
        public long CompanyCostCenterId { get; set; }
        public long? CompanyId { get; set; }
        public long? CostCentreId { get; set; }
        public double? BrokerMarkup { get; set; }
        public double? ContactMarkup { get; set; }
        public bool? isDisplayToUser { get; set; }
        public long? OrganisationId { get; set; }
        public virtual Company Company { get; set; }
        public virtual CostCentre CostCentre { get; set; }
    }
}
