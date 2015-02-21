using System.Collections.Generic;

namespace MPC.Models.DomainModels
{
    public class Currency
    {
        public long CurrencyId { get; set; }
        public string CurrencyCode { get; set; }
        public string CurrencyName { get; set; }
        public string CurrencySymbol { get; set; }

        /// <summary>
        /// Organisations using this currency
        /// </summary>
        public virtual ICollection<Organisation> Organisations { get; set; } 
    }
}
