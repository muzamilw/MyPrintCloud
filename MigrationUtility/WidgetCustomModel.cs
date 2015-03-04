using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationUtility
{
    public class CmsSkinPageWidgetModel
    {
        public long PageWidgetId { get; set; }
        public long? PageId { get; set; }
        public long? WidgetId { get; set; }
        public long? SkinId { get; set; }
        public short? Sequence { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }
        public string ParamValue { get; set; }
       
    }
}
