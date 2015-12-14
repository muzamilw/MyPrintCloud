using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MPC.Models.Common;


namespace MPC.MIS.Areas.Api.Models
{
    public class CostCenterExecutionParamRequest
    {
        public ItemSection CurrentItemSection { get; set; }
        public QuestionAndInputQueues Queues { get; set; }
    }
}