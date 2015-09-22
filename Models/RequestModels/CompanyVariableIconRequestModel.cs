using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.RequestModels
{
    public class CompanyVariableIconRequestModel : GetPagedListRequest
    {
        public string IconBytes { get; set; }

        public string IconName { get; set; }
        public long VariableId { get; set; }

        public string VariableName { get; set; }

        public long CompanyId { get; set; }
    }
}
