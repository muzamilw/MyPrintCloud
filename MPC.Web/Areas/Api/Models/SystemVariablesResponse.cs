using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// System Variables Response
    /// </summary>
    public class SystemVariablesResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of System Variables
        /// </summary>
        public IEnumerable<SystemVariableForListView> SystemVariables { get; set; }
    }
}