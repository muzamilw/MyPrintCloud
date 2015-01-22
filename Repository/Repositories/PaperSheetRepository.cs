using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.Common;
using MPC.Models.DomainModels;
using MPC.Models.RequestModels;
using MPC.Models.ResponseModels;
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
        public PaperSheetResponse SearchPaperSheet(PaperSheetRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
            Expression<Func<PaperSize, bool>> query =
                paperSize =>
                    (isStringSpecified && paperSize.Name.Contains(request.SearchString) || !isStringSpecified);

            var rowCount = DbSet.Count(query);
            var paperSheets= request.IsAsc
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
            PaperSheetResponse paperSheetResponse = new PaperSheetResponse
            {
                PaperSizes = paperSheets,
                RowCount = rowCount
            };
            return paperSheetResponse;
        }
         
        #endregion
    }
}
