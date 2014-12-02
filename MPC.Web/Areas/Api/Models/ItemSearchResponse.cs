using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Search Response Web Reponse
    /// </summary>
    public class ItemSearchResponse
    {
        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<Item> Items { get; set; }
        
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}