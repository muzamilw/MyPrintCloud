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
    public class GoodRecieveNoteRepository : BaseRepository<GoodsReceivedNote>, IGoodRecieveNoteRepository
    {
        #region Private
        private readonly Dictionary<GoodsReceivedNoteByColumn, Func<GoodsReceivedNote, object>> goodsReceivedNoteOrderByClause = new Dictionary<GoodsReceivedNoteByColumn, Func<GoodsReceivedNote, object>>
                    {
                        {GoodsReceivedNoteByColumn.PurchaseId, d => d.PurchaseId},
                        {GoodsReceivedNoteByColumn.Code, c => c.code},
                        //{GoodsReceivedNoteByColumn.Name, d => d.s},
                        {GoodsReceivedNoteByColumn.DateReceived, d => d.date_Received},
                        {GoodsReceivedNoteByColumn.GrandTotal, d => d.grandTotal}
                    };
        #endregion
        public GoodRecieveNoteRepository(IUnityContainer container)
            : base(container)
        {
        }
        protected override IDbSet<GoodsReceivedNote> DbSet
        {
            get
            {
                return db.GoodsReceivedNotes;
            }
        }

        /// <summary>
        /// Get Goods Received Notes For Specified Search
        /// </summary>
        public GoodsReceivedNoteResponseModel GetGoodsReceivedNotes(GoodsReceivedNoteRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            
            Expression<Func<GoodsReceivedNote, bool>> query =
                item =>
                    (item.SupplierId != null && item.SupplierId == request.CompanyId);

            IEnumerable<GoodsReceivedNote> items = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(goodsReceivedNoteOrderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                   .OrderByDescending(goodsReceivedNoteOrderByClause[request.ItemOrderBy])
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();

            return new GoodsReceivedNoteResponseModel { GoodsReceivedNotes = items, TotalCount = DbSet.Count(query) };
        }
    }
}
