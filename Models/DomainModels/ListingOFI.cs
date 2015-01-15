using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// Listing OFI Domain Model
    /// </summary>
    public class ListingOFI
    {
        public long ListingOFIId { get; set; }
        public long? ListingId { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string OFIRef { get; set; }
        public string ThirdPartyRef { get; set; }
        public string PropertyRef { get; set; }
    }
}
