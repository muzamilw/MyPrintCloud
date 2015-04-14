using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MPC.Repository.Repositories
{
    public class PurchaseRepository : BaseRepository<Purchase>, IPurchaseRepository
    {
        #region Private
        private readonly Dictionary<PurchaseByColumn, Func<Purchase, object>> purchaseOrderByClause = new Dictionary<PurchaseByColumn, Func<Purchase, object>>
                    {
                        {PurchaseByColumn.PurchaseId, d => d.PurchaseId},
                        {PurchaseByColumn.Code, c => c.Code},
                        //{GoodsReceivedNoteByColumn.Name, d => d.s},
                        {PurchaseByColumn.DatePurchase, d => d.date_Purchase},
                        {PurchaseByColumn.TotalPrice, d => d.TotalPrice}
                    };
        #endregion
        public PurchaseRepository(IUnityContainer container)
            : base(container)
        {
        }
        protected override IDbSet<Purchase> DbSet
        {
            get
            {
                return db.Purchases;
            }
        }
        /// <summary>
        /// Get Purchases For Specified Search
        /// </summary>
        public PurchaseResponseModel GetPurchases(PurchaseRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;

            Expression<Func<Purchase, bool>> query =
                item =>
                    (item.SupplierId != null && item.SupplierId == request.CompanyId);

            IEnumerable<Purchase> items = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(purchaseOrderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(purchaseOrderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            return new PurchaseResponseModel { Purchases = items, TotalCount = DbSet.Count(query) };
        }
    }
}
