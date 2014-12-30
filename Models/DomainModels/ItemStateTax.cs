namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Item State Tax Domain Model
    /// </summary>
    public class ItemStateTax
    {
        public long ItemStateTaxId { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public double? TaxRate { get; set; }
        public long? ItemId { get; set; }

        public virtual Item Item { get; set; }
        public virtual Country Country { get; set; }
        public virtual State State { get; set; }
    }
}
