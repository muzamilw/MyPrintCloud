using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class ReportparamResponse
    {
        public Reportparam param { get; set; }
        public List<ReportparamComboCollection> ComboList { get; set; }
    }
}