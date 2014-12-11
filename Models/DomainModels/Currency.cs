namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Currency Domain Model
    /// </summary>
    public class Currency
    {
        public long CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
    }
}
