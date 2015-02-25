using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Company Contact Variable API Model
    /// </summary>
    public class CompanyContactVariable
    {
        public long ContactVariableId { get; set; }
        public long ContactId { get; set; }
        public long VariableId { get; set; }
        public string Value { get; set; }
        public int? Type { get; set; }
        public string Title { get; set; }
        public long? FakeVariableId { get; set; }

        public List<VariableOption> VariableOptions { get; set; }

    }
}