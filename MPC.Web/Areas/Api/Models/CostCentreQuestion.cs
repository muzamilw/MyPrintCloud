using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCentreQuestion
    {
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public short? Type { get; set; }
        public string DefaultAnswer { get; set; }
        public int CompanyId { get; set; }
        public int SystemSiteId { get; set; }

        public string VariableString { get; set; }
    }
}