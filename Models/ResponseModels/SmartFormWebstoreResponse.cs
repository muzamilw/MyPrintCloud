using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.ResponseModels
{
    public class SmartFormWebstoreResponse
    {
        public long SmartFormId { get; set; }
        public string Name { get; set; }
        public string Heading { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }
    }
    public class SmartFormDetailWebstoreResponse
    {
        public long SmartFormDetailId { get; set; }
        public long SmartFormId { get; set; }
        public int? ObjectType { get; set; }
        public bool? IsRequired { get; set; }
        public int? SortOrder { get; set; }
        public long? VariableId { get; set; }
        public string CaptionValue { get; set; }

        public virtual FieldVariableWebstoreResposne FieldVariable { get; set; }


    }
    public class FieldVariableWebstoreResposne
    {
        public long VariableId { get; set; }
        public string VariableName { get; set; }
        public string RefTableName { get; set; }
        public string CriteriaFieldName { get; set; }
        public long? VariableSectionId { get; set; }
        public string VariableTag { get; set; }
        public int? SortOrder { get; set; }
        public string KeyField { get; set; }
        public int? VariableType { get; set; }
        public int? Scope { get; set; }
        public string WaterMark { get; set; }
        public string DefaultValue { get; set; }
        public string InputMask { get; set; }
        public long? CompanyId { get; set; }
        public long? OrganisationId { get; set; }
        public bool? IsSystem { get; set; }
        public string VariableTitle { get; set; }
        public virtual ICollection<VariableExtensionWebstoreResposne> VariableOptions { get; set; }
 

    }
    public class VariableExtensionWebstoreResposne
    {
        public int Id { get; set; }
        public long? FieldVariableId { get; set; }
        public int? CompanyId { get; set; }
        public int? OrganisationId { get; set; }
        public string VariablePrefix { get; set; }
        public string VariablePostfix { get; set; }
        public bool? CollapsePrefix { get; set; }
        public bool? CollapsePostfix { get; set; }
    }
}
