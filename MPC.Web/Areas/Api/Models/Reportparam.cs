using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class Reportparam
    {
        public int ParmId { get; set; }
        public string ParmName { get; set; }
        public string Caption1 { get; set; }
        public int ReportId { get; set; }
        public int ControlType { get; set; }
        public string ComboTableName { get; set; }
        public string ComboIDFieldName { get; set; }
        public string ComboTextFieldName { get; set; }
        public string CriteriaFieldName { get; set; }
        public string OrderByFieldName { get; set; }
        public int? SameAsPArmId { get; set; }
        public string Caption2 { get; set; }
        public int? Operator { get; set; }
        public int? LogicalOperator { get; set; }
        public string DefaultValue1 { get; set; }
        public string DefaultValue2 { get; set; }
        public double? MinValue { get; set; }
        public double? MaxValue { get; set; }
        public int? FilterType { get; set; }
        public int? SortOrder { get; set; }
    }
}