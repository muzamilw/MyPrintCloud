using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Field Variable API Response
    /// </summary>
    public class FieldVariableResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Field Variable
        /// </summary>
        public IEnumerable<FieldVariable> FieldVariables { get; set; }
    }
}