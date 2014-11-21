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
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class PaperSheetRepository: BaseRepository<PaperSize>, IPaperSheetRepository
    {
        #region Private
        private readonly Dictionary<PaperSheetByColumn, Func<PaperSize, object>> cityOrderByClause = new Dictionary<PaperSheetByColumn, Func<PaperSize, object>>
                    {
                        {PaperSheetByColumn.Name, d => d.Name},
                        {PaperSheetByColumn.Height, c => c.Height},
                        {PaperSheetByColumn.Width, d => d.Width},
                        {PaperSheetByColumn.SizeMeasure, d => d.SizeMeasure},
                        {PaperSheetByColumn.Area, d => d.Region},
                        {PaperSheetByColumn.IsFixed, d => d.IsFixed},
                        {PaperSheetByColumn.Region, d => d.Region},
                        {PaperSheetByColumn.IsArchived, d => d.IsArchived},
                        
                    };
        #endregion
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public PaperSheetRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<PaperSize> DbSet
        {
            get
            {
                return db.PaperSizes;
            }
        }

        #endregion
        #region Public
        /// <summary>
        /// Search Paper Sheet
        /// </summary>
        /// <param name="request"></param>
        /// <param name="rowCount"></param>
        /// <returns></returns>
        public IEnumerable<PaperSize> SearchPaperSheet(PaperSheetRequestModel request, out int rowCount)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            Expression<Func<PaperSize, bool>> query =
                paperSize =>
                    (string.IsNullOrEmpty(request.SearchString));

            rowCount = DbSet.Count(query);
            return request.IsAsc
                ? DbSet.Where(query)
                    .OrderBy(cityOrderByClause[request.PaperSheetOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                    .OrderByDescending(cityOrderByClause[request.PaperSheetOrderBy])
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
        }
        #endregion
    }
}
