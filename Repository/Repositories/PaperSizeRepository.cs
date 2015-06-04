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
using AutoMapper;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// Paper Size Repository
    /// </summary>
    public class PaperSizeRepository : BaseRepository<PaperSize>, IPaperSizeRepository
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
        public PaperSizeRepository(IUnityContainer container)
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
        /// Get All Paper Size
        /// </summary>
        public override IEnumerable<PaperSize> GetAll()
        {
           // Organisation org = db.Organisations.Where(o => o.OrganisationId == this.OrganisationId).FirstOrDefault();
            //string sCulture = org.GlobalLanguage != null ? org.GlobalLanguage.culture : string.Empty;
            return DbSet.Where(s => s.OrganisationId == OrganisationId).OrderBy(s => s.Area).ToList();
        }

        /// <summary>
        /// Search Paper Sheet
        /// </summary>
        public PaperSheetResponse SearchPaperSheet(PaperSheetRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStringSpecified = !string.IsNullOrEmpty(request.SearchString);
            Expression<Func<PaperSize, bool>> query =
                paperSize =>
                    ((isStringSpecified && paperSize.Name.Contains(request.SearchString) || !isStringSpecified)
                    && paperSize.OrganisationId == OrganisationId && paperSize.Region == request.Region && (paperSize.IsArchived == false || paperSize.IsArchived == null));

            var rowCount = DbSet.Count(query);
            var paperSheets = request.IsAsc
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

        public List<PaperSize> GetPaperByOrganisation(long OrganisationID)
        {
            try
            {


                return db.PaperSizes.Where(o => o.OrganisationId == OrganisationID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public void SavePaperSizes(List<PaperSize> sizes, long OrganisationID)
        {
            try
            {
                foreach (var size in sizes)
                {
                    size.OrganisationId = OrganisationID;
                    db.PaperSizes.Add(size);
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<PaperSize> GetPaperSizesByID(int PSSID)
        {
            try
            {
                return db.PaperSizes.Where(c => c.PaperSizeId == PSSID).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        #endregion
    }
}
