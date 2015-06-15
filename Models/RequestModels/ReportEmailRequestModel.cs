﻿using MPC.Models.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.RequestModels
{
    public class ReportEmailRequestModel
    {

        public long Reportid { get; set; }

        public Guid SignedBy { get; set; }

        public long ContactId { get; set; }

        public long RecordId { get; set; }

        public ReportType ReportType { get; set; }

        public long OrderId { get; set; }

        public string CriteriaParam { get; set; }

       
    }
}
