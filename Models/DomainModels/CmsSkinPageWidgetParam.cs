using System;

namespace MPC.Models.DomainModels
{
    public class CmsSkinPageWidgetParam
    {
        public long PageWidgetParamId { get; set; }
        public long? PageWidgetId { get; set; }
        public string ParamName { get; set; }
        public string ParamValue { get; set; }

        public virtual CmsSkinPageWidget CmsSkinPageWidget { get; set; }
    }
}
