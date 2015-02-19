using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Field Variable Response
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
