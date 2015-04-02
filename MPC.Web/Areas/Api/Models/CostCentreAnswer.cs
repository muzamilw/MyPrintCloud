using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MPC.MIS.Areas.Api.Models
{
    public class CostCentreAnswer
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public double? AnswerString { get; set; }
    }
}