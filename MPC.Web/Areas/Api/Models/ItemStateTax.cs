namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item State Tax WebApi Model
    /// </summary>
    public class ItemStateTax
    {
        public long ItemStateTaxId { get; set; }
        public long? CountryId { get; set; }
        public long? StateId { get; set; }
        public double? TaxRate { get; set; }
        public long? ItemId { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
    }
}