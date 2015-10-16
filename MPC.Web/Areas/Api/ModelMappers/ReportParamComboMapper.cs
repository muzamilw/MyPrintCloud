using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    public static class ReportParamComboMapper
    {
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static ReportparamComboCollection CreateFrom(this MPC.Models.Common.ReportparamComboCollection source)
        {
            return new ReportparamComboCollection
            {
               ComboId = source.ComboId,
               ComboText = source.ComboText
            };
        }

        /// <summary>
        /// Create From Client Model
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static MPC.Models.Common.ReportparamComboCollection CreateFrom(this ReportparamComboCollection source)
        {
            return new MPC.Models.Common.ReportparamComboCollection
            {
                ComboId = source.ComboId,
                ComboText = source.ComboText
            };
        }

    }
}