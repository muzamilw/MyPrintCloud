using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Item Search Response Model
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
