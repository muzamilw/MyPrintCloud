using MPC.Models.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPC.Models.Common
{
    public class CostCentreQuestions
    {
        public int Id { get; set; }
        public string QuestionString { get; set; }
        public short? Type { get; set; }
        public string DefaultAnswer { get; set; }
        public int CompanyId { get; set; }
        public int SystemSiteId { get; set; }
        public List<CostCentreAnswer> AnswerCollection { get; set; }
    }
}
