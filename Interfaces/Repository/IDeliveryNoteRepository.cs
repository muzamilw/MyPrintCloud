using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;

namespace MPC.Interfaces.Repository
{
    /// <summary>
    /// DeliveryNote Repository 
    /// </summary>
    public interface IDeliveryNoteRepository : IBaseRepository<DeliveryNote, int>
    {
        /// <summary>
        /// Get Delivery Notes
        /// </summary>
        GetDeliveryNoteResponse GetOrders(GetOrdersRequest request);
    }
}
