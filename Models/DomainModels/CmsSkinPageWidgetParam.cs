using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CmsSkinPageWidgetParam
    {
        public int PageWidgetParamId { get; set; }
        public Nullable<int> PageWidgetId { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }

        public virtual CmsSkinPageWidget CmsSkinPageWidget { get; set; }
    }
}
