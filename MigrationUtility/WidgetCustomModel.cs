using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MigrationUtility
{
    public class CmsSkinPageWidgetModel
    {
    
        public long? PageId { get; set; }
        public string PageName { get; set; }
        public long? WidgetId { get; set; }
        public long? SkinId { get; set; }
        public short? Sequence { get; set; }
        public string ParamValue { get; set; }
       
    }
}
