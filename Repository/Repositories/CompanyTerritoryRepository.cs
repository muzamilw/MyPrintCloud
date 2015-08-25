using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Models.ResponseModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class CompanyTerritoryRepository : BaseRepository<CompanyTerritory>, ICompanyTerritoryRepository
    {
        public CompanyTerritoryRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CompanyTerritory> DbSet
        {
            get
            {
                return db.CompanyTerritories;
            }
        }
        public Models.ResponseModels.CompanyTerritoryResponse GetCompanyTerritory(Models.RequestModels.CompanyTerritoryRequestModel request)
        {
            int fromRow = (request.PageNo - 1) * request.PageSize;
            int toRow = request.PageSize;
            bool isStringSpecified = !string.IsNullOrEmpty(request.SearchFilter);
            Expression<Func<CompanyTerritory, bool>> query =
                s =>
                    (isStringSpecified && (s.TerritoryName.Contains(request.SearchFilter)) || (s.TerritoryCode.Contains(request.SearchFilter)) ||
                     !isStringSpecified) && (s.CompanyId == request.CompanyId);

            int rowCount = DbSet.Count(query);
            // ReSharper disable once ConditionalTernaryEqualBranch
            IEnumerable<CompanyTerritory> companyTerritories = request.IsAsc
                ? DbSet.Where(query)
                .OrderByDescending(x => x.CompanyId)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList()
                : DbSet.Where(query)
                .OrderByDescending(x => x.CompanyId)
                    .Skip(fromRow)
                    .Take(toRow)
                    .ToList();
            return new CompanyTerritoryResponse
            {
                RowCount = rowCount,
                CompanyTerritories = companyTerritories
            };
        }

        public IEnumerable<CompanyTerritory> GetAllCompanyTerritories(long companyId)
        {
            return DbSet.Where(x => x.CompanyId == companyId).ToList();
        }
        public CompanyTerritory GetTerritoryById(long territoryId)
        {
            try
            {
                return db.CompanyTerritories.Where(c => c.TerritoryId == territoryId).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public IEnumerable<CompanyTerritory> GetAllCompanyTerritoriesWithStoreID(long companyId,long StoreID)
        {
            return DbSet.Where(x =>x.CompanyId == companyId).ToList();
        }
    }
}
