﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class usp_ChartEstimateToOrderConversionCount_Result
    {
        public Nullable<int> TotalEstimateCount { get; set; }
        public Nullable<int> Month { get; set; }
        public string monthname { get; set; }
        public Nullable<int> year { get; set; }
    }
}
