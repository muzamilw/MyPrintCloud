using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
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
