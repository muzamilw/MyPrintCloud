﻿using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public partial class CostCentreQuestion
    {
        public List<CostCentreAnswer> AnswerCollection { get; set; }
    }
}