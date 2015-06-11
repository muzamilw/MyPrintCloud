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

        /// <summary>
        /// Get Purchase Orders
        /// </summary>
        public PurchaseResponseModel GetPurchaseOrders(PurchaseOrderSearchRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStatusSpecified = request.Status == 0;//if true get all then get by status

            Expression<Func<Purchase, bool>> query =
            item =>
                (
                string.IsNullOrEmpty(request.SearchString) ||
                ((item.Company != null && item.Company.Name.Contains(request.SearchString)) || (item.RefNo.Contains(request.SearchString))
                )) && (!isStatusSpecified && item.Status == request.Status || isStatusSpecified) && (item.isproduct.HasValue && item.isproduct == request.PurchaseOrderType);

            IEnumerable<Purchase> items = DbSet.Where(query)
                .OrderBy(x => x.date_Purchase)
                .Skip(fromRow)
                .Take(toRow)
                .ToList();
            return new PurchaseResponseModel { Purchases = items, TotalCount = DbSet.Count(query) };
        }

        /// <summary>
        /// Generate PO By SP
        /// </summary>
        public bool GeneratePO(long OrderId,Guid CreatedBy)
        {
            try
            {
                db.usp_GeneratePurchaseOrders((int)OrderId, CreatedBy);

                return true;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }


        /// <summary>
        /// Delete PO By SP
        /// </summary>
        public bool DeletePO(long OrderId)
        {
            try
            {
                db.usp_DeletePurchaseOrders((int)OrderId);
               
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        /// <summary>
        /// Get Purchases List
        /// </summary>
        public Dictionary<int, long> GetPurchasesList(long OrderId)
        {
            try
            {
                Dictionary<int, long> DictPurchases = new Dictionary<int, long>();
                var ListPurchases = (from i in db.Items
                                     join izs in
                                         (from p in db.Purchases
                                          join pd in this.db.PurchaseDetails on p.PurchaseId equals pd.PurchaseId
                                          select new
                                          {
                                              ItemID = pd.ItemId,
                                              PurchaseID = p.PurchaseId,
                                              SupplierID = p.SupplierId

                                          }) on i.ItemId equals izs.ItemID
                                     where i.EstimateId == OrderId
                                     select new
                                     {
                                         PurchaseID = izs.PurchaseID,
                                         SupplierID = izs.SupplierID

                                     }).Distinct();


                foreach(var purchase in ListPurchases)
                {
                    DictPurchases.Add(purchase.PurchaseID, purchase.SupplierID ?? 0);
                }
                return DictPurchases;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


      
    }
}
