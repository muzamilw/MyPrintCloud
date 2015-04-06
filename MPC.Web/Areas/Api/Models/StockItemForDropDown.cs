using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class StockItemForDropDown
    {
        /// <summary>
        /// Stock Item ID
        /// </summary>
        public long StockItemId { get; set; }

        /// <summary>
        /// Item Code
        /// </summary>
        public string ItemCode { get; set; }

        /// <summary>
        /// Item Name
        /// </summary>
        public string ItemName { get; set; }

        /// <summary>
        /// Alternate Name
        /// </summary>
        public string AlternateName { get; set; }

    }
}