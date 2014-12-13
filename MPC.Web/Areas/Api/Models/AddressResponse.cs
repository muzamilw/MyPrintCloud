using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    public class AddressResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Companies
        /// </summary>
        public IEnumerable<Address> Addresses { get; set; }
    }
}