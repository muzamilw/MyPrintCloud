using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MPC.Models.Common;

namespace MPC.Models.RequestModels
{
    public class CostCenterExecutionRequest
    {
        public long CostCenterId { get; set; }
        public long ItemId { get; set; }
        public int Quantity { get; set; }
        public string Action { get; set; }
        public List<QuestionQueueItem> QuestionQueueItems { get; set; }
    }
}
