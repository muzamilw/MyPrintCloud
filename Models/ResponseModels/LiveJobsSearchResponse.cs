using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Live Jobs Domain SearchResponse
    /// </summary>
    public class LiveJobsSearchResponse
    {

        public List<Item> Items { get; set; }

        public int TotalCount { get; set; }
    }
}
