using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.Models.DomainModels;

namespace MPC.Webstore.Models
{
    public class CmsSkinPageWidget
    {
        public long PageWidgetId { get; set; }
        public long? PageId { get; set; }
        public long? WidgetId { get; set; }
        public long? SkinId { get; set; }
        public short? Sequence { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }

        public virtual Company Company { get; set; }
        public virtual Organisation Organisation { get; set; }
        public virtual Widget Widget { get; set; }
        public virtual ICollection<CmsSkinPageWidgetParam> CmsSkinPageWidgetParams { get; set; }
    }
}