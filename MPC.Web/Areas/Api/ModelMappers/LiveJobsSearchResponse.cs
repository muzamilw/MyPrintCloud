using System.Collections.Generic;
using MPC.MIS.Areas.Api.Models;

namespace MPC.MIS.Areas.Api.ModelMappers
{
    /// <summary>
    /// Live Job Api Search Response
    /// </summary>
    public class LiveJobsSearchResponse
    {
        public List<LiveJobItem> Items { get; set; }

        public int TotalCount { get; set; }
    }
}