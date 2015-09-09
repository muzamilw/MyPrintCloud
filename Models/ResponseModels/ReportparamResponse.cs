using MPC.Models.Common;
using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.ResponseModels
{
    public class ReportparamResponse
    {
        public Reportparam param { get; set; }
        public List<ReportparamComboCollection> ComboList { get; set; }
    }
}
