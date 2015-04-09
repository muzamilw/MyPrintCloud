using System;

namespace MPC.Models.RequestModels
{
    /// <summary>
    /// Activity Request Model
    /// </summary>
    public class ActivityRequestModel
    {
        public DateTime? StartDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public Guid UserId { get; set; }

    }
}
