using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class BreadCrumbModel
    {
        /// <summary>
        /// Product Category Id
        /// </summary>
        public long CrumbId { get; set; }

        /// <summary>
        /// Parent CategoryId
        /// </summary>
        public int CrumbParentId { get; set; }

        /// <summary>
        /// Category name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Sequence
        /// </summary>
        public int Sequence { get; set; }

        /// <summary>
        /// Enabled?
        /// </summary>
        public bool IsEnable { get; set; }

        /// <summary>
        /// Url
        /// </summary>
        public string Url { get; set; }
        
    }
}
