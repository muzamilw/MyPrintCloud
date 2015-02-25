using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Common
{
    public class VariableList
    {
        public string SectionName { get; set; }
        public long VariableID { get; set; }
        public string VariableName { get; set; }
        public string VariableTag { get; set; }
        public int VariableType { get; set; } 
    }
    public class TemplateVariablesObj
    {
        public string VariableTag { get; set; }
        public long VariableID { get; set; }
        public long TemplateID { get; set; }

        public TemplateVariablesObj (string VaraibaleTag, long VariableId, int TemplateId)
        {
            this.VariableTag = VaraibaleTag;
            this.VariableID = VariableId;
            this.TemplateID = TemplateId;

        }
    }
}
