using System.Linq.Expressions;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using System;
using System.Collections.Generic;

namespace MPC.Interfaces.Repository
{
    public interface IPurchaseRepository : IBaseRepository<Purchase, long>
    {
        PurchaseResponseModel GetPurchases(PurchaseRequestModel request);

        /// <summary>
        /// Get Purchase Orders
        /// </summary>
        PurchaseResponseModel GetPurchaseOrders(PurchaseOrderSearchRequestModel request);

        bool GeneratePO(long OrderId, Guid CreatedBy);

        Dictionary<int, long> GetPurchasesList(long OrderId);

        bool DeletePO(long OrderId);
        /// <summary>
        /// Load Property
        /// </summary>
        void LoadProperty<T>(object entity, Expression<Func<T>> propertyExpression, bool isCollection = false);

       
    }
}
