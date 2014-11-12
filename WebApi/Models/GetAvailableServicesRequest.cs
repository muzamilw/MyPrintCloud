using System;

namespace MPC.WebApi.Models
{
    public class GetAvailableServicesRequest
    {
        /// <summary>
        /// Start Date Time
        /// </summary>
        public DateTime StartDateTime { get; set; }
        /// <summary>
        /// End Date Time
        /// </summary>
        public DateTime EndDateTime { get; set; }
        /// <summary>
        /// Out Location OpeartionWorkplaceId
        /// </summary>
        public long OutLocationId { get; set; }
        /// <summary>
        /// Return Location Operation Workplace Id
        /// </summary>
        public long ReturnLocationId { get; set; }

        /// <summary>
        /// Hire Group Detail Id
        /// </summary>
        public long HireGroupDetailId { get; set; }

        /// <summary>
        /// Domain Key
        /// </summary>
        public long DomainKey { get; set; }
    }
}