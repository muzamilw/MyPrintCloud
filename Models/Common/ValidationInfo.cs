﻿using System;
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

        public bool IsTrial { get; set; }
        public int TrialDaysLeft { get; set; }
    }
}
