using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CmsSkinPageWidget
    {
        public int PageWidgetId { get; set; }
        public Nullable<int> PageId { get; set; }
        public Nullable<int> WidgetId { get; set; }
        public Nullable<int> SkinId { get; set; }
        public Nullable<short> Sequence { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> OrganisationId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Widget Widget { get; set; }
        public virtual ICollection<CmsSkinPageWidgetParam> CmsSkinPageWidgetParams { get; set; }
    }
}
