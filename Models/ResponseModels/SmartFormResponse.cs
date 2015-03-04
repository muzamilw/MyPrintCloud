using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Smart Form Domain Response
    /// </summary>
    public class SmartFormResponse
    {
        /// <summary>
        /// Row Count
        /// </summary>
        public int RowCount { get; set; }

        /// <summary>
        /// List of Smart Form
        /// </summary>
        public IEnumerable<SmartForm> SmartForms { get; set; }
    }
}
