using System.Collections.Generic;

namespace MPC.MIS.Areas.Api.Models
{
    /// <summary>
    /// Item Search Response For Order Web Reponse
    /// </summary>
    public class ItemSearchResponseForOrder
    {
        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<ItemListViewForOrder> Items { get; set; }
        
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}