﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class ReportManagerResponseModel
    {
        public ReportCategory ReportCategory { get; set; }
        public List<Report> Reports { get; set; }


    }
}