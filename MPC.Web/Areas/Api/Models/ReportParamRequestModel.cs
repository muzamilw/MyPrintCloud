using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class ReportParamRequestModel
    {
        public long ReportId { get; set; }

        public int ComboValue { get; set; }


         public string ParamDateFromValue { get; set; }

          public string ParamDateToValue { get; set; }

        public string ParamTextBoxValue { get; set; }

    }
}