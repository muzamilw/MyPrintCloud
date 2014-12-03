using System.Collections.Generic;
using MPC.Models.DomainModels;

namespace MPC.Models.ResponseModels
{
    /// <summary>
    /// Item List View Response Model
    /// </summary>
    public class ItemListViewSearchResponse
    {
        /// <summary>
        /// Items
        /// </summary>
        public IEnumerable<GetItemsListView> Items { get; set; }
        
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}
