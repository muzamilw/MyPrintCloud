using MPC.MIS.Areas.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace MPC.MIS.Areas.Api.ModelMappers
{
    using DomainModels = MPC.Models.DomainModels;
    public static class ItemsForDiscountVoucherMapper
    {
         
        /// <summary>
        /// Create From Domain Model
        /// </summary>
        public static IEnumerable<Item> CreateFrom(this IEnumerable<MPC.Models.DomainModels.Item> source)
        {
            return source == null ? new List<Item>() : source.Select(pc => pc.CreateFrom());
        }
        
    }
}