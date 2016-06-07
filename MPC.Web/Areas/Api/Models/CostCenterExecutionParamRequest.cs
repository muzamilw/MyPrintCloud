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
        public string CostCentreId { get; set; }
        public string ClonedItemId { get; set; }
        public string OrderedQuantity { get; set; }
        public string CallMode { get; set; }
    }
}