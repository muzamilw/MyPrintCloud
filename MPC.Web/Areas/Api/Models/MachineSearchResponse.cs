using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Machine Search Response
    /// </summary>
    public class MachineSearchResponse
    {
        /// <summary>
        ///  Machines
        /// </summary>
        public IEnumerable<Machine> Machines { get; set; }


        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
