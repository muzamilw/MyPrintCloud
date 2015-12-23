using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class DeliveryNoteRepository : BaseRepository<DeliveryNote>, IDeliveryNoteRepository
    {
        public DeliveryNoteRepository(IUnityContainer container)
            : base(container)
        {
        }

        /// <summary>
        /// Item Orderby clause
        /// </summary>
        private readonly Dictionary<DeliveryNoteByColumn, Func<DeliveryNote, object>> _deliveryNoteByClause =
            new Dictionary<DeliveryNoteByColumn, Func<DeliveryNote, object>>
            {
                {DeliveryNoteByColumn.Name, c => c.CustomerOrderReff},
                {DeliveryNoteByColumn.Code, c => c.DeliveryDate}
            };

        protected override IDbSet<DeliveryNote> DbSet
        {
            get { return db.DeliveryNotes; }
        }

        public DeliveryNote Find(int id)
        {
            return DbSet.Find(id);
        }

        public GetDeliveryNoteResponse GetDeliveryNotes(DeliveryNotesRequest request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;

            Expression<Func<DeliveryNote, bool>> query =
                item =>
                    ((string.IsNullOrEmpty(request.SearchString) ||
                      item.CustomerOrderReff.Contains(request.SearchString)) && item.IsStatus == request.Status && item.OrganisationId == OrganisationId);
            IEnumerable<DeliveryNote> deliveryNotes = request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(_deliveryNoteByClause[request.ItemOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(_deliveryNoteByClause[request.ItemOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();

            return new GetDeliveryNoteResponse { DeliveryNotes = deliveryNotes, TotalCount = DbSet.Count(query) };
        }
    }
}
