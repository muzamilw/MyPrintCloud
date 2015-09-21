using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public partial class vw_CompanyVariableIcons
    {
        public long variableid { get; set; }
        public string variablename { get; set; }
        public string variabletag { get; set; }
        public Nullable<long> CompanyId { get; set; }
        public Nullable<long> OrganisationId { get; set; }
        public Nullable<long> ContactCompanyId { get; set; }
        public string Icon { get; set; }
    }
}
