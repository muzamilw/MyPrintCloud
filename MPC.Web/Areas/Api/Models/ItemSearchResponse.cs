﻿using System.Collections.Generic;

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
        public IEnumerable<ItemListView> Items { get; set; }
        
        /// <summary>
        /// Total Count
        /// </summary>
        public int TotalCount { get; set; }
    }
}