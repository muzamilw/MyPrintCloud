using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;
using MPC.Models.RequestModels;
using System.Linq.Expressions;
using MPC.Models.ResponseModels;

namespace MPC.Repository.Repositories
{
    public class CompanyCostCenterRepository : BaseRepository<CompanyCostCentre>, ICompanyCostCenterRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CompanyCostCenterRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CompanyCostCentre> DbSet
        {
            get
            {
                return db.CompanyCostCentres;
            }
        }

        #endregion
        #region Public
         /// <summary>
        /// Get Company Cost Centres for Company ID
        /// </summary>
        public CostCentreResponse GetCompanyCostCentreByCompanyId(GetCostCentresRequest request)
        {

           int fromRow = (request.PageNo - 1) * request.PageSize;
           int toRow = request.PageSize;
           bool isSearchFilterSpecified = !string.IsNullOrEmpty(request.SearchString);
           Expression<Func<CompanyCostCentre, bool>> query =
               s =>
                   (isSearchFilterSpecified && (s.CostCentre.Name.Contains(request.SearchString)) ||
                    (s.CostCentre.HeaderCode.Contains(request.SearchString)) ||
                    !isSearchFilterSpecified)
                    && (s.CompanyId == request.CompanyId);

           int rowCount = DbSet.Count(query);
           // ReSharper disable once ConditionalTernaryEqualBranch
           IEnumerable<CompanyCostCentre> costCentres = request.IsAsc
               ? DbSet.Where(query)
                   .OrderBy(x => x.CostCentre.Name)
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList()
               : DbSet.Where(query)
                    .OrderBy(x => x.CostCentre.Name)
                   .Skip(fromRow)
                   .Take(toRow)
                   .ToList();
           return new CostCentreResponse
           {
               RowCount = rowCount,
               CostCentres = costCentres
           };

        }
        #endregion
    }
}
