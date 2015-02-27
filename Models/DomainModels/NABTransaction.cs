using System;

namespace MPC.Models.DomainModels
{
    /// <summary>
    /// NAB Transaction Domain Model
    /// </summary>
    public class NABTransaction
    {
        public long Id { get; set; }
        public int? EstimateId { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime? datetime { get; set; }
    }
}
