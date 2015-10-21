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
        public int? VariableType { get; set; }

        public bool? CollapsePostfix { get; set; }
        public bool? CollapsePrefix { get; set; }

        public VariableList(string sectionName, long variableId, string variableName, string variableTag, int? variableType, bool? CollapsePrefix, bool? CollapsePostfix)
        {
            this.SectionName = sectionName;
            this.VariableID = variableId;
            this.VariableTag = variableTag;
            this.VariableName = variableName;
            this.VariableType = variableType;
            this.CollapsePostfix = CollapsePostfix;
            this.CollapsePrefix = CollapsePrefix;
        }
    }
    public class TemplateVariablesObj
    {
        public string VariableTag { get; set; }
        public long VariableID { get; set; }
        public long TemplateID { get; set; }
        public string VariableText { get; set; }

        public TemplateVariablesObj(string VaraibaleTag, long VariableId, long TemplateId, string VariableText)
        {
            this.VariableTag = VaraibaleTag;
            this.VariableID = VariableId;
            this.TemplateID = TemplateId;
            this.VariableText = VariableText;
        }
    }

    public class SmartFormUserList
    {
        public long ContactId { get; set; }
        public string ContactName { get; set; }

        public SmartFormUserList(long contactId, string contactName)
        {
            this.ContactId = contactId;
            this.ContactName = contactName;
        }
    }

    public class RealEstateImage
    {
        public long ImageId { get; set; }
        public string ImageUrl { get; set; }
    }
}
