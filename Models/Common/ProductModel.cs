using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class ProductModel
    {
        /// <summary>
        /// Category Id
        /// </summary>
        public long CategoryId { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string ProductName { get; set; }

        /// <summary>
        /// Item Id
        /// </summary>
        public long ItemId { get; set; }

        /// <summary>
        /// Product Name
        /// </summary>
        public string ThumbnailPath { get; set; }
    }
}
