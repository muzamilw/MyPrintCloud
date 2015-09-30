using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public partial class vw_CompanyVariableIcons
    {
        public long variableid { get; set; }
        public string variablename { get; set; }
        public string variabletag { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public Nullable<long> VariableIconId { get; set; }
        public Nullable<long> ContactCompanyId { get; set; }
        public string Icon { get; set; }
    }
}