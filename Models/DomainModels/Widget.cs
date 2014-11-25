using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class Widget
    {
        public long WidgetId { get; set; }
        public string WidgetCode { get; set; }
        public string WidgetName { get; set; }
        public string WidgetControlName { get; set; }

        public virtual ICollection<CmsSkinPageWidget> CmsSkinPageWidgets { get; set; }
    }
}
