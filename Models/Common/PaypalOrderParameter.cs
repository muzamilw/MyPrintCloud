﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class PaypalOrderParameter
    {
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
        public double TotalQuantity { get; set; }
    }
}
