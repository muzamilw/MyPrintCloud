﻿using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    public class CostCenterTypeRepository : BaseRepository<CostCentreType>, ICostCenterTypeRepository
    {
        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        public CostCenterTypeRepository(IUnityContainer container)
            : base(container)
        {

        }
        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<CostCentreType> DbSet
        {
            get
            {
                return db.CostCentreTypes;
            }
        }

        #endregion
        #region Public
        
        public override IEnumerable<CostCentreType> GetAll()
        {
            return DbSet.Where(c => c.CompanyId == OrganisationId).ToList();
        }
        public CostCentreType GetCostCenterTypeById(int Id)
        {
            return DbSet.Where(s => s.TypeId == Id).FirstOrDefault();
        }
        #endregion
    }
}
