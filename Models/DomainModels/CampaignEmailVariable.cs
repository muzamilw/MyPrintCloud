using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.DomainModels
{
    public class CampaignEmailVariable
    {
        public long VariableId { get; set; }
        public string VariableName { get; set; }
        public string RefTableName { get; set; }
        public string RefFieldName { get; set; }
        public string CriteriaFieldName { get; set; }
        public string Description { get; set; }
        public int? SectionId { get; set; }
        public string VariableTag { get; set; }
        public string Key { get; set; }
        public long? OrganisationId { get; set; }
        public virtual Section Section { get; set; }
    }
}
