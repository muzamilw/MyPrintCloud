using System;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Model while displaying customer list
    /// </summary>
    public class CustomerListViewModel
    {
        /// <summary>
        /// Company Id
        /// </summary>
        public long  CompnayId { get; set; }

        /// <summary>
        /// Customer Name
        /// </summary>
        public string CustomerName { get; set; }

        /// <summary>
        /// Customer Creation date
        /// </summary>
        public DateTime? DateCreted { get; set; }

        /// <summary>
        /// Customer Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Customer Email
        /// </summary>
        public string Email { get; set; }
    }
}