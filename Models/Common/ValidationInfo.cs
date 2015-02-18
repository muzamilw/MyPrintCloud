using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class ValidationInfo
    {
        public string userId { get; set; }
        public string CustomerID { get; set; }

        public string FullName { get; set; }

        public string Plan { get; set; }
        public string Email { get; set; }

        /// <summary>
        /// Defines if user is in trial period
        /// </summary>
        public Boolean IsTrial { get; set; }

        /// <summary>
        /// Trial period count
        /// </summary>
        public int TrialCount { get; set; }
    }
}
