﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class ReportCategoryRequestModel
    {
        public long CategoryId { get; set; }
        public int IsExternal { get; set; }
        
    }
}