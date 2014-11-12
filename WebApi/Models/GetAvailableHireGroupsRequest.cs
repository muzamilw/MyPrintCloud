using System;

namespace MPC.WebApi.Models
{
    /// <summary>
    /// Web Api Model for Getting Available HireGroup Reequest
    /// </summary>
    public sealed class GetAvailableHireGroupsRequest
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
        /// Domain Key
        /// </summary>
        public long DomainKey { get; set; }
    }
}