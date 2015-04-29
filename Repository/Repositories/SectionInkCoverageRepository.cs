﻿using System.Data.Entity;
using Microsoft.Practices.Unity;
using MPC.Interfaces.Repository;
using MPC.Models.DomainModels;
using MPC.Repository.BaseRepository;

namespace MPC.Repository.Repositories
{
    /// <summary>
    /// SectionInkCoverage Repository
    /// </summary>
    public class SectionInkCoverageRepository : BaseRepository<SectionInkCoverage>, ISectionInkCoverageRepository
    {
        #region privte
        #endregion

        #region Constructor

        /// <summary>
        /// Constructor
        /// </summary>
        public SectionInkCoverageRepository(IUnityContainer container)
            : base(container)
        {

        }

        /// <summary>
        /// Primary database set
        /// </summary>
        protected override IDbSet<SectionInkCoverage> DbSet
        {
            get
            {
                return db.SectionInkCoverages;
            }
        }

        #endregion

        #region public

        /// <summary>
        /// Find by Id
        /// </summary>
        public SectionInkCoverage Find(int id)
        {
            return base.Find(id);
        }
        
        #endregion
        
    }
}